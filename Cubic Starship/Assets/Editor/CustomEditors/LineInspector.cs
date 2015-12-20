using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor
{

	void OnSceneGUI()
    {
        Line line = target as Line;
        Transform handleTransform = line.transform;
        //Quaternion handleRotation = line.transform.rotation;
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

        Vector3 point0 = handleTransform.TransformPoint(line.point0);
        Vector3 point1 = handleTransform.TransformPoint(line.point1);

        Handles.color = Color.white;
        Handles.DrawLine(point0, point1);

        EditorGUI.BeginChangeCheck();
        point0 = Handles.PositionHandle(point0, handleRotation);
        point1 = Handles.PositionHandle(point1, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            line.point0 = handleTransform.InverseTransformPoint(point0);
            line.point1 = handleTransform.InverseTransformPoint(point1);
        }

        //EditorGUI.BeginChangeCheck();
        //point1 = Handles.PositionHandle(point1, handleRotation);
        //if (EditorGUI.EndChangeCheck())
        //{
        //    line.point1 = handleTransform.InverseTransformPoint(point1);
        //}
    }
}
