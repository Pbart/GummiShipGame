using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(BezierCurve))]
public class BezierCurveInspector : Editor
{
    private BezierCurve b_curve;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const int lineSteps = 10;
    private const float directionScale = 0.5f;

    void OnSceneGUI()
    {
        b_curve = target as BezierCurve;
        handleTransform = b_curve.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3 point0 = ShowPoint(0);
        Vector3 point1 = ShowPoint(1);
        Vector3 point2 = ShowPoint(2);
        Vector3 point3 = ShowPoint(3);

        Handles.color = Color.gray;
        Handles.DrawLine(point0, point1);
        Handles.DrawLine(point1, point2);
        Handles.DrawLine(point2, point3);

        if (b_curve.showDirections == true)
        {
            ShowDirections();
        }
        Handles.DrawBezier(point0, point3, point1, point2, Color.white, null, 2f);
        
        //Handles.color = Color.white;
        //Vector3 lineStart = b_curve.GetPoint(0f);
        //Handles.color = Color.green;
        //Handles.DrawLine(lineStart, lineStart + b_curve.GetDirection(0f));

        //for (int i = 1; i < lineSteps; i++)
        //{
        //    Vector3 lineEnd = b_curve.GetPoint(i / (float)lineSteps);
        //    Handles.color = Color.white;
        //    Handles.DrawLine(lineStart, lineEnd);
        //    if (b_curve.showDirections == true)
        //    {
        //        Handles.color = Color.green;
        //        Handles.DrawLine(lineEnd, lineEnd + b_curve.GetDirection(i / (float)lineSteps));
        //    }
        //    lineStart = lineEnd;
        //}
    }
    private Vector3 ShowPoint(int pointIndex)
    {
        Vector3 point = handleTransform.TransformPoint(b_curve.points[pointIndex]);
        EditorGUI.BeginChangeCheck();
        point = Handles.PositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(b_curve, "Move Point");
            EditorUtility.SetDirty(b_curve);
            b_curve.points[pointIndex] = handleTransform.InverseTransformPoint(point);
        }
        return point;
    }

    private void ShowDirections()
    {
        Handles.color = Color.green;
        Vector3 point = b_curve.GetPoint(0f);
        Handles.DrawLine(point, point + b_curve.GetDirection(0f) * directionScale);
        for (int i = 1; i <= lineSteps; i++)
        {
            point = b_curve.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(point, point + b_curve.GetDirection(i / (float)lineSteps) * directionScale);
        }
    }
 }
