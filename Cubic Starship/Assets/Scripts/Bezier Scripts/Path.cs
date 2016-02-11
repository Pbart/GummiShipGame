using UnityEngine;
using System.Collections;

abstract public class Path : MonoBehaviour 
{
	abstract public Vector3 GetPoint(float t);			//returns a point in space on the path at time t
	abstract public Vector3 GetVelocity(float t);		//returns the velocity on the path at time t
	abstract public Vector3 GetDirection(float t);		//returns the direction as a unit vector on the path at time t



}
