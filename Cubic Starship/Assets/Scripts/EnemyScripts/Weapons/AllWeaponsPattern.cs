using UnityEngine;
using System.Collections;

public class AllWeaponsPattern : WeaponsPatternBase 
{
	public EnemyWeapon[] weapons;

	public override void ShootWeapons()
	{
		//print ("shooting all waeapons!");
		for(int i = 0; i < weapons.Length; i++)
		{
			weapons[i].Shoot();
		}
	}
}
