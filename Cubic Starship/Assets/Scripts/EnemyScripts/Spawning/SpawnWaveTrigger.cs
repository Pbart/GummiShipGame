using UnityEngine;
using System.Collections;

//simply triggers the enemy waves specified when the trigger is entered
public class SpawnWaveTrigger : MonoBehaviour {

	public EnemySpawnWave[] enemySpawnWaves;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			for(int i = 0; i < enemySpawnWaves.Length; i++)
			{
				enemySpawnWaves[i].TriggerWave();
			}
		}
	}

}
