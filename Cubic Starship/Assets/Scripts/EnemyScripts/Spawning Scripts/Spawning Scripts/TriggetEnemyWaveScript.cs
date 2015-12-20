using UnityEngine;
using System.Collections;

//simply triggers the enemy waves specified when the trigger is entered
public class TriggetEnemyWaveScript : MonoBehaviour {

	public EnemySpawnWave[] enemyWaves;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			for(int i = 0; i < enemyWaves.Length; i++)
			{
				enemyWaves[i].TriggerWave();
			}
		}
	}

}
