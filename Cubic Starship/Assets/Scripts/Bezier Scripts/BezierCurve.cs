using UnityEngine;
using System.Collections;

//// Formulas for a Bezier Curve: 
//// Linear: B(t) = (1-t)P0 + tP1
//// Quadratic:  B(t) = (1 - t)2 P0 + 2 (1 - t) t P1 + t2 P2.
//// Cubic: B(t) = (1 - t)3 P0 + 3 (1 - t)2 t P1 + 3 (1 - t) t2 P2 + t3 P3

//public static class Bezier
//{
//    //Quadratic Curve
//    public static Vector3 GetPoint(Vector3 point0, Vector3 point1, Vector3 point2, float t)
//    {
//        t = Mathf.Clamp01(t);
//        float oneMinusT = 1f - t;
//        return oneMinusT * oneMinusT * point0 +
//               2f * oneMinusT * t * point1 +
//               t * t * point2;
//    }

//    //Cubic Curve
//    public static Vector3 GetPoint(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, float t)
//    {
//        t = Mathf.Clamp01(t);
//        float oneMinusT = 1f - t;
//        return 
//            oneMinusT * oneMinusT * oneMinusT * point0 +
//            3f * oneMinusT * oneMinusT * t * point1 +
//            3f * oneMinusT * t * t * point2 +
//            t * t * t * point3;
//    }

//    //Derivative of the Quadratic Curve
//    public static Vector3 GetFirstDerivative(Vector3 point0, Vector3 point1, Vector3 point2, float t)
//    {
//        return
//            2f * (1f - t) * (point1 - point0) +
//            2f * t * (point2 - point1);
//    }

//    //Derivative of the Cubic Curve
//    public static Vector3 GetFirstDerivative(Vector3 point0, Vector3 point1, Vector3 point2, Vector3 point3, float t)
//    {
//        t = Mathf.Clamp01(t);
//        float oneMinusT = 1f - t;
//        return
//            3f * oneMinusT * oneMinusT * (point1 - point0) +
//            6f * oneMinusT * t * (point2 - point1) +
//            3f * t * t * (point3 - point2);
//    }
//}

public class BezierCurve : MonoBehaviour
{
    public Vector3[] points;
    public bool showDirections = true;

    public void Reset()
    {
        points = new Vector3[] {
            new Vector3(1f, 0, 0),
            new Vector3(2f, 0, 0),
            new Vector3(3f, 0, 0),
            new Vector3(4f, 0, 0)};
    }

    //for the Quadratic Curve
    //public Vector3 GetPoint(float t)
    //{
    //    return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], t));
    //}

    //for the Cubic Curve
    public Vector3 GetPoint(float t)
    {
        return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], points[3], t));
    }

    //for the Quadratic Curve
    //public Vector3 GetVelocity(float t)
    //{
    //    return transform.TransformPoint(Bezier.GetFirstDerivative(points[0], points[1], points[2], t)) - transform.position;
    //}

    //for the Cubic Curve
    public Vector3 GetVelocity(float t)
    {
        return transform.TransformPoint(Bezier.GetFirstDerivative(points[0], points[1], points[2], points[3], t)) - transform.position;
    }
    
    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }
}
