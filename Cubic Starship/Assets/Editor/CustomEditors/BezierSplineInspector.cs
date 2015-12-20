using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor
{
    private const int lineSteps = 10;
    private const int stepsPerCurve = 10;

    private const float directionScale = 0.5f;
    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;

    private bool showDirections;

    private int selectedIndex = -1;

    private BezierSpline b_spline;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private static Color[] modeColors = { Color.white, Color.yellow, Color.cyan };

    public override void OnInspectorGUI()
    {
        b_spline = target as BezierSpline;
        //showDirections = GUILayout.Toggle(showDirections, "Show Direction");
        EditorGUI.BeginChangeCheck();
        showDirections = EditorGUILayout.Toggle("Show Directions", b_spline.showDirections);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(b_spline, "Toogle Show Directions");
            EditorUtility.SetDirty(b_spline);
            b_spline.showDirections = showDirections;
        }
        EditorGUI.BeginChangeCheck();
        bool looped = EditorGUILayout.Toggle("Loop", b_spline.IsLooped);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(b_spline, "Toogle Loop");
            EditorUtility.SetDirty(b_spline);
            b_spline.IsLooped = looped;
        }
        if (selectedIndex >= 0 && selectedIndex < b_spline.ControlPointCount)
        {
            DrawSelectedPointInspector();
        }
        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(b_spline, "Add Curve");
            b_spline.AddCurve();
            EditorUtility.SetDirty(b_spline);
        }
    }

    private void DrawSelectedPointInspector()
    {
        GUILayout.Label("Selected Point");
        EditorGUI.BeginChangeCheck();
        Vector3 point = EditorGUILayout.Vector3Field("Position", b_spline.GetControlPoint(selectedIndex));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(b_spline, "Move Point");
            EditorUtility.SetDirty(b_spline);
            b_spline.SetControlPoint(selectedIndex, point);
        }
        EditorGUI.BeginChangeCheck();
        BezierControlPointMode mode = (BezierControlPointMode)EditorGUILayout.EnumPopup("Mode", b_spline.GetControlPointMode(selectedIndex));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(b_spline, "Change Point Mode");
            b_spline.SetControlPointMode(selectedIndex, mode);
            EditorUtility.SetDirty(b_spline);
        }
    }

    void OnSceneGUI()
    {
        b_spline = target as BezierSpline;
        handleTransform = b_spline.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3 point0 = ShowPoint(0);
        for (int i = 1; i < b_spline.ControlPointCount; i += 3)
        {
            Vector3 point1 = ShowPoint(i);
            Vector3 point2 = ShowPoint(i + 1);
            Vector3 point3 = ShowPoint(i + 2);

            Handles.color = Color.gray;
            Handles.DrawLine(point0, point1);
            Handles.DrawLine(point2, point3);

            Handles.DrawBezier(point0, point3, point1, point2, Color.white, null, 2f);
            point0 = point3;
        }
        if (showDirections == true)
        {
            ShowDirections();
        }
        
    }
    private Vector3 ShowPoint(int pointIndex)
    {
        Vector3 point = handleTransform.TransformPoint(b_spline.GetControlPoint(pointIndex));
        float size = HandleUtility.GetHandleSize(point);
        if (pointIndex == 0)
        {
            size *= 2f;
        }
        Handles.color = modeColors[(int)b_spline.GetControlPointMode(pointIndex)];
        if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotCap))
        {
            selectedIndex = pointIndex;
            Repaint();
        }
        if (selectedIndex == pointIndex)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.PositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(b_spline, "Move Point");
                EditorUtility.SetDirty(b_spline);
                b_spline.SetControlPoint(pointIndex, handleTransform.InverseTransformPoint(point));
            }
        }
        return point;
    }

    private void ShowDirections()
    {
        Handles.color = Color.green;
        Vector3 point = b_spline.GetPoint(0f);
        Handles.DrawLine(point, point + b_spline.GetDirection(0f) * directionScale);
        int steps = stepsPerCurve * b_spline.BezierCurveCount;
        for (int i = 1; i <= steps; i++)
        {
            point = b_spline.GetPoint(i / (float)steps);
            Handles.DrawLine(point, point + b_spline.GetDirection(i / (float)steps) * directionScale);
        }
    }
}
