using UnityEngine;
using System.Collections;

//bullet that goes straight on its positive z axis (forward)
public class StraightBullet : EnemyProjectile 
{
	public float speed = 5f;
	private Vector3 m_ToPlayer;

	public override void Awake()
	{
		base.Awake();
	}

	// Update is called once per frame
	override public void Update () 
	{
		this.transform.position += this.transform.forward*speed;
	}
}
