using UnityEngine;
using System.Collections;

//This is the base class for each enemy projectiles
abstract public class EnemyProjectile : MonoBehaviour
{
	public float projectileLifetime = 5;	//seconds until projectile is destroyed

	virtual public void Awake()
	{
		Destroy(gameObject, projectileLifetime);
	}

	public virtual void Update()
	{
		//intentionally empty so it can be overriden
	}
}
