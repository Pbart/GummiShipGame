using UnityEngine;
using System.Collections;

public enum WeaponState
{
	Idle,
	Firing
}

//This class is responsitble for spawning the enemyProjectiles at the specified fire rate.
//It is mainly to be used with a WeaponsPattern in conjunction with a WeaponsGroupController, but can be fired from any script/animation by calling Shoot().
public class EnemyWeapon : MonoBehaviour 
{
	public EnemyProjectile projectile;		//projectile to fire
	public bool debugShoot = false;
	public int numberOfProjectiles;			//number of shots everytime Shoot() is called
	public float fireRate = 1f; 			//projectile firing frequency, units in shots/sec
	public bool playerSpace = true;			//true if you want the bullet to be in player space, false if you want it to be in world space
	public float m_FirePeriod{get; set;}	//seconds between each shot, calculated internally (1/fireRate)
	private float m_TimeElapsed;			//time elapsed once this starts shooting
	private int m_BulletsFired;				//number of bullets fired so far (once it starts shooting)
	private WeaponState m_State;


	// fires the numberOfProjectiles at the fireRate frequency
	public void Shoot()
	{
		if(m_State != WeaponState.Firing)
		{
			FireProjectile(); //fires first bullet
			m_State = WeaponState.Firing;
		}
	}
	

	// Use this for initialization
	void Awake () 
	{
		ResetWeapon();

	}



	void Update () 
	{
		if(debugShoot)
		{
			debugShoot = false;
			FireProjectile();
		}

		switch(m_State)
		{
		case WeaponState.Idle:
		{
			break;
		}
		case WeaponState.Firing:
		{
			m_TimeElapsed += Time.deltaTime;

			if(m_TimeElapsed > m_FirePeriod)
			{
				FireProjectile();
				//m_TimeElapsed -= m_FirePeriod;	//1-25-16 Eric - I would use this becuase its more "correct" way to fire....but it creates annoying breaks in firing at high fire rate (doesn't look smooth)
				m_TimeElapsed = 0f;					// Instead, just setting it back to 0
			}

			if(numberOfProjectiles <= m_BulletsFired)
			{
				ResetWeapon();
			}

			break;
		}
		}

	}

	void ResetWeapon()
	{
		if(fireRate <= 0)
			fireRate = 1f;
		m_FirePeriod = 1f/fireRate;
		m_TimeElapsed = 0f;
		m_BulletsFired = 0;
		m_State = WeaponState.Idle;
	}


	void FireProjectile()
	{
		if(projectile)
		{
			Instantiate(projectile, this.transform.position, this.transform.rotation);
			m_BulletsFired++;
		}
	}

	public float TotalTimeForShooting()
	{
		return fireRate * (numberOfProjectiles-1);
	}

}
