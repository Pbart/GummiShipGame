using UnityEngine;
using System.Collections;

public enum SpawnerState
{
	SpawnerIdle,
	SpawnerSpawning,
	SpawnerFinished
}

//interface for each specific spawner. EnemyWaveSpawn uses this to trigger the spawn and to tell when a specific spawner is done.
public abstract class ISpawner : MonoBehaviour 
{
	abstract public void TriggerSpawn();			//used to activate spawner
	abstract public float TotalTimeForSpawner();	//returns the total amount of time the spawner will take (used in timing enemy spawners in EnemySpawnWave class)


	public bool IsFinished() {
		return m_State == SpawnerState.SpawnerFinished;
	}

	protected SpawnerState m_State;


}
