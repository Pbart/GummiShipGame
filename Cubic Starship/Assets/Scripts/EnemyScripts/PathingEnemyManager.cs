using UnityEngine;
using System.Collections;


public class PathingEnemyManager : BaseEnemyManager 
{
	public float shootWeaponGroupTimeInterval = 4f;
	public string weaponGroupName;				//1/29/16 Eric - keep this a string, becuase eventually the Animation Controller's animations will call the wgController.fireWeapons, and that requires a string.
	private WeaponGroupController wgController;
	private float m_ShootingTimeElapsed = 0f;

	public override void Awake()
	{
		base.Awake();

		wgController = GetComponent<WeaponGroupController>();
	}

	public override void Kill()
	{
		base.Kill ();
	}

	public override void Despawn()
	{
		base.Despawn();
	}

	// Update is called once per frame
	public override void Update () 
	{

		switch(baseState)
		{
		
		case BaseEnemyState.ALIVE:
		{
			m_ShootingTimeElapsed += Time.deltaTime;

			if(m_ShootingTimeElapsed > shootWeaponGroupTimeInterval)
			{
				wgController.ShootWeaponGroup(weaponGroupName);
				m_ShootingTimeElapsed = 0f;
			}

			break;
		}
		}

		base.Update();

	}
}
