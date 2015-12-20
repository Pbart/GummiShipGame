using UnityEngine;
using System;
using System.Collections;

public enum BezierControlPointMode
{
    Free,
    Aligned,
    Mirrored
}

// Formulas for a Bezier Curve: 
// Linear: B(t) = (1-t)P0 + tP1
// Quadratic:  B(t) = (1 - t)2 P0 + 2 (1 - t) t P1 + t2 P2.
// Cubic: B(t) = (1 - t)3 P0 + 3 (1 - t)2 t P1 + 3 (1 - t) t2 P2 + t3 P3

public static class Bezier
{
    //Quadratic Curve
    public static Vector3 GetPoint(Vector3 point0, Vector3 point1, Vector3 point2, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * point0 +
               2f * oneMinusT * t * point1 +
               t * t * point2;
    }

    //Cubic Curve
    public static Vector3 GetPoint(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * point0 +
            3f * oneMinusT * oneMinusT * t * point1 +
            3f * oneMinusT * t * t * point2 +
            t * t * t * point3;
    }

    //Derivative of the Quadratic Curve
    public static Vector3 GetFirstDerivative(Vector3 point0, Vector3 point1, Vector3 point2, float t)
    {
        return
            2f * (1f - t) * (point1 - point0) +
            2f * t * (point2 - point1);
    }

    //Derivative of the Cubic Curve
    public static Vector3 GetFirstDerivative(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (point1 - point0) +
            6f * oneMinusT * t * (point2 - point1) +
            3f * t * t * (point3 - point2);
    }
}

public class BezierSpline : MonoBehaviour
{
    
    public bool showDirections;

    [SerializeField]
    private Vector3[] points;

    [SerializeField]
    private bool isLooped;

    [SerializeField]
    private BezierControlPointMode[] modes;

    public void Reset()
    {
        points = new Vector3[] {
            new Vector3(1f, 0, 0),
            new Vector3(2f, 0, 0),
            new Vector3(3f, 0, 0),
            new Vector3(4f, 0, 0)};
        modes = new BezierControlPointMode[]
            {
                BezierControlPointMode.Free, BezierControlPointMode.Free
            };
    }

    public void AddCurve()
    {
        Vector3 newPoint = points[points.Length - 1];
        Array.Resize(ref points, points.Length + 3);

        //try to make this go in the direction of the curve
        newPoint.x += 1f;
        points[points.Length - 3] = newPoint;
        newPoint.x += 1f;
        points[points.Length - 2] = newPoint;
        newPoint.x += 1f;
        points[points.Length - 1] = newPoint;

        Array.Resize(ref modes, modes.Length + 1);
        modes[modes.Length - 1] = modes[modes.Length - 2];
        EnforceMode(points.Length - 4);

        if (isLooped)
        {
            points[points.Length - 1] = points[0];
            modes[modes.Length - 1] = modes[0];
            EnforceMode(0);
        }
    }

    //for the Cubic Curve
    public Vector3 GetPoint(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * BezierCurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetPoint(points[i], points[i + 1], points[i + 2], points[i + 3], t));
    }

    //for the Cubic Curve
    public Vector3 GetVelocity(float t)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = points.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * BezierCurveCount;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        return transform.TransformPoint(Bezier.GetFirstDerivative(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
    }

    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }

    public int BezierCurveCount
    {
        get { return (points.Length - 1) / 3; }
    }

    public int ControlPointCount
    {
        get { return points.Length; }
    }

    public bool IsLooped
    {
        get { return isLooped; }
        set { isLooped = value;
              if (value == true)
              {
                  modes[modes.Length - 1] = modes[0];
                SetControlPoint(0, points[0]);
              }
            }
    }

    public Vector3 GetControlPoint(int pointIndex)
    {
        return points[pointIndex];
    }

    public void SetControlPoint(int pointIndex, Vector3 point)
    {
        if (pointIndex % 3 == 0)
        {
            Vector3 delta = point - points[pointIndex];
            if (isLooped)
            {
                if (pointIndex == 0)
                {
                    points[1] += delta;
                    points[points.Length - 2] += delta;
                    points[points.Length - 1] = point;
                }
                else if (pointIndex == points.Length - 1)
                {
                    points[0] = point;
                    points[1] += delta;
                    points[pointIndex - 1] += delta;
                }
                else
                {
                    points[pointIndex - 1] += delta;
                    points[pointIndex + 1] += delta;
                }
            }
            else
            {
                if (pointIndex > 0)
                {
                    points[pointIndex - 1] += delta;
                }
                if (pointIndex + 1 < points.Length)
                {
                    points[pointIndex + 1] += delta;
                }
            }
        }
        points[pointIndex] = point;
        EnforceMode(pointIndex);
    }

    public BezierControlPointMode GetControlPointMode(int pointIndex)
    {
        return modes[(pointIndex + 1) / 3];
    }

    public void SetControlPointMode(int pointIndex, BezierControlPointMode mode)
    {
        int modeIndex = (pointIndex + 1) / 3;
        modes[modeIndex] = mode;
        if (isLooped)
        {
            if (modeIndex == 0)
            {
                modes[modes.Length - 1] = mode;
            }
            else if (modeIndex == modes.Length - 1)
            {
                modes[0] = mode;
            }
        }
        EnforceMode(pointIndex);
    }

    private void EnforceMode(int pointIndex)
    {
        int modeIndex = (pointIndex + 1) / 3;
        BezierControlPointMode mode = modes[modeIndex];
        if (mode == BezierControlPointMode.Free || !isLooped && modeIndex == 0 || modeIndex == modes.Length - 1)
        {
            return;
        }

        int fixedIndex, enforcedIndex;
        int middleIndex = modeIndex * 3;

        if (pointIndex <= middleIndex)
        {
            fixedIndex = middleIndex - 1;
            if (fixedIndex < 0)
            {
                fixedIndex = points.Length - 2;
            }
            enforcedIndex = middleIndex + 1;
            if (fixedIndex >= points.Length)
            {
                enforcedIndex = 1;
            }
        }
        else
        {
            fixedIndex = middleIndex + 1;
            if (fixedIndex >= points.Length)
            {
                fixedIndex = 1;
            }
            enforcedIndex = middleIndex - 1;
            if (enforcedIndex < 0)
            {
                enforcedIndex = points.Length - 2;
            }
        }

        Vector3 middlePoint = points[middleIndex];
        Vector3 enforcedTangent = middlePoint - points[fixedIndex];
        if (mode == BezierControlPointMode.Aligned)
        {
            enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middlePoint, points[enforcedIndex]);
        }
        points[enforcedIndex] = middlePoint + enforcedTangent;
    }
}
