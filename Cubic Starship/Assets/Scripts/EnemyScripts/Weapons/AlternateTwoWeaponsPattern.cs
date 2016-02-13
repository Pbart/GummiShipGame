using UnityEngine;
using System.Collections;

public enum WeaponGroupStart
{
	LeftGroup,
	RightGroup
};

public class AlternateTwoWeaponsPattern : WeaponsPatternBase 
{
	public EnemyWeapon[] weaponsGroupLeft;
	public EnemyWeapon[] weaponsGroupRight;
	public WeaponGroupStart startingGroup;

	private float m_OffsetTimeBetweenGroups;
	private float m_TimeElapsed;
	private bool m_CurrentlyFiring = false;

	private EnemyWeapon[] m_FirstToFireGroup;
	private EnemyWeapon[] m_SecondToFireGroup;

	void Update()
	{
		if(m_CurrentlyFiring)
		{
			m_TimeElapsed += Time.deltaTime;
			if(m_TimeElapsed >= m_OffsetTimeBetweenGroups)
			{
				FireGroupWeapons(m_SecondToFireGroup);
				print ("offset: " + m_OffsetTimeBetweenGroups);
				print ("time elapsed: " + m_TimeElapsed);
				Reset ();
			}
		}
	}

	public override void ShootWeapons ()
	{
		if(!m_CurrentlyFiring)
		{
			//if 2 or more weapons exist
			if(weaponsGroupLeft[0] && weaponsGroupRight[0])
			{
				//find out witch weapon group to shoot first and second
				if(startingGroup == WeaponGroupStart.LeftGroup)
				{
					m_FirstToFireGroup = weaponsGroupLeft;
					m_SecondToFireGroup = weaponsGroupRight;
				}
				else
				{
					m_FirstToFireGroup = weaponsGroupRight;
					m_SecondToFireGroup = weaponsGroupLeft;
				}

				//fire the first group
				FireGroupWeapons(m_FirstToFireGroup);

				//calculate offset time to fire the second group
				m_OffsetTimeBetweenGroups = m_SecondToFireGroup[0].m_FirePeriod/2f;
				m_CurrentlyFiring = true;


			}
			//if only 1 weapon exists
			else if(weaponsGroupLeft[0] || weaponsGroupRight[0])
			{
				if(weaponsGroupLeft[0])
				{
					FireGroupWeapons(weaponsGroupLeft);
				}
				else
				{
					FireGroupWeapons(weaponsGroupRight);
				}

				Reset ();
			}
			//no weapons exist
			else
			{
				Reset ();
			}
		}
	}


	void FireGroupWeapons(EnemyWeapon[] group)
	{
		for(int i = 0; i < group.Length; i++)
		{
			group[i].Shoot();
		}
	}


	void Reset()
	{
		m_CurrentlyFiring = false;
		m_OffsetTimeBetweenGroups = 0f;
		m_TimeElapsed = 0f;
		m_FirstToFireGroup = null;
		m_SecondToFireGroup = null;
	}
}
