using UnityEngine;
using System.Collections;

public class PathingEnemyMovement : BaseEnemyMovement
{
	public float despawnTimeAfterPath = 0f;
	private float m_TimeElapsed = 0f;

	// Use this for initialization
	public override void Awake () 
	{
		base.Awake();
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();
	}

	public override void MovementUpdate ()
	{
		m_TimeElapsed += Time.deltaTime;

		if(m_TimeElapsed >= despawnTimeAfterPath)
		{
			manager.Despawn();
		}
	}
}
