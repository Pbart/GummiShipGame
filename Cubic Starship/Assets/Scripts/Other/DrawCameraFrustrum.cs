using UnityEngine;
using System.Collections;

public class DrawCameraFrustrum : MonoBehaviour 
{
	public bool drawFrustrum;
	public Camera cameraObject;
	public Color cameraColor;

	void OnDrawGizmos()
	{
		if(drawFrustrum)
		{
			if(cameraObject)
			{
				Gizmos.color = cameraColor;
				Gizmos.matrix = cameraObject.gameObject.transform.localToWorldMatrix;
				Gizmos.DrawFrustum(cameraObject.gameObject.transform.position, cameraObject.fieldOfView, cameraObject.farClipPlane, cameraObject.nearClipPlane, cameraObject.aspect);

			}
		}
	}
}
