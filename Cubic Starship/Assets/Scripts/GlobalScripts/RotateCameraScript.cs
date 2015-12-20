using UnityEngine;
using System.Collections;

public class RotateCameraScript : MonoBehaviour
{
    private float yRot;
    public float rotSpeed;
	
	// Update is called once per frame
	void Update ()
    {
        yRot += rotSpeed;
        Camera.main.transform.rotation = Quaternion.Euler(0, yRot, 0);
	}
}
