using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour 
{
	public int health = 100;
	public RenderDamagedController renderDamageController;
	public float damageFlashTime = 0.1f;
	private BaseEnemyManager m_Manager;
	private float m_FlashTimeElapsed = 0f;
	private bool m_Damaged = false;

	void Awake()
	{
		m_Manager = GetComponent<BaseEnemyManager>();
	}

	void OnTriggerEnter(Collider col)
	{

		if(col.gameObject.tag == "Projectile")
		{
			PlayerBullet bullet;
			bullet = col.gameObject.GetComponent<PlayerBullet>();

			if(bullet && m_Manager.IsAlive())
			{
				//GET DAMAGE FROM PLAYER BULLET HERE (temp 10 dmg)
				//health -= bullet.Damage;
				health -= 10;

				//show flashing of ship to tell player it was hurt
				renderDamageController.ShowDamagedMaterial();
				m_FlashTimeElapsed = 0f;
				m_Damaged = true;

				//destroy bullet here? (not sure if it should be here or inside Playerbullet)

				if(health <= 0)
				{
					//tell the manager it is dead
					m_Manager.Kill ();
				}

			}
		}
	}

	void Update()
	{
		//show the flash until the flash time is reached
		if(m_Damaged)
		{
			m_FlashTimeElapsed += Time.deltaTime;
			if(m_FlashTimeElapsed >= damageFlashTime)
			{
				m_Damaged = false;
				renderDamageController.ShowOriginalMaterial();
			}
		}
	}

}














