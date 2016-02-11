using UnityEngine;
using System.Collections;

//this just simply pivots the current transform to look at the player
public class LookAtPlayer : MonoBehaviour 
{
	private Transform m_PlayerTransform;

	// Use this for initialization
	void Start () 
	{
		m_PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.LookAt(m_PlayerTransform.position);
	}
}
