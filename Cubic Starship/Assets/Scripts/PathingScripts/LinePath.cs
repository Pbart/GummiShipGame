using UnityEngine;
using System.Collections;

public class LinePath : MonoBehaviour 
{
	public Vector3 p1;
	public Vector3 p2;

	private float t = 0;	//the progress along the line

	public void progress()
	{
		if(t >= 0)
			t = 0f;


	}

}
