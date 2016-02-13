using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponGroupController : MonoBehaviour 
{
	private Dictionary<string, WeaponsPatternBase> m_WeaponDictionary = new Dictionary<string, WeaponsPatternBase>();

	// Use this for initialization
	void Awake () 
	{
		WeaponsPatternBase[] w = this.GetComponents<WeaponsPatternBase>();

		for(int i = 0; i < w.Length; i++)
		{
			m_WeaponDictionary.Add(w[i].patternName, w[i]);
		}
	}

	public void ShootWeaponGroup(string groupName)
	{
		WeaponsPatternBase w = m_WeaponDictionary[groupName];
		if(w)
		{
			print ("Shooting weapon pattern: " + groupName);
			w.ShootWeapons();
		}
	}

}
