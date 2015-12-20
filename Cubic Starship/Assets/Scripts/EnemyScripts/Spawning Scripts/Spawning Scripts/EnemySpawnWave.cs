using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WaveState
{
	WaveIdle,
	WaveSpawning,
	WaveFinished
}

//This class organizes the EnemySpawners to trigger at customizable times.
//The information for each Spawner is kept in a seperate class called SpawnNode.
public class EnemySpawnWave : MonoBehaviour {

	public EnemySpawnNode[] spawnersInOrder;
	public bool forceSpawnWave; //temp for testing and debugging
	
	private WaveState m_State;
	private int m_SpawnerIndex;			//current index in spawner List
	private float m_TimeElapsed;		//time elapsed from start of wave
	private float m_TimeEnd;			//time to stop the wave and reset

	void Start ()
	{
		WaveInit();
	}
	
	void WaveInit()
	{
		m_State = WaveState.WaveIdle;
		m_SpawnerIndex = 0;
		m_TimeElapsed = 0;
		m_TimeEnd = 0;
	}


	void Update()
	{
		switch (m_State) 
		{
		case WaveState.WaveSpawning:
			{
				m_TimeElapsed += Time.deltaTime;
				
				bool cont = true;

				if(m_SpawnerIndex < spawnersInOrder.Length)
				{
					//see if there are any spawners that need to be activated
					while(cont)
					{
						//no more spanwers to activate
						if(m_SpawnerIndex >= spawnersInOrder.Length)
						{
							cont = false;
							break;
						}
						// there is a spawner to activate, activate and continue
						else if(m_TimeElapsed >= spawnersInOrder[m_SpawnerIndex].timeStart)
						{
							spawnersInOrder[m_SpawnerIndex].spawner.TriggerSpawn();
							m_SpawnerIndex++;
						}
						//no spawners to activate currently
						else
						{
							cont = false;
						}
						
						
					}
				}
				
				//reset the state if wave is finished
				if(m_TimeElapsed > m_TimeEnd)
				{
					m_State = WaveState.WaveFinished;
				}

				break;
			}
		case WaveState.WaveFinished:
			{
				WaveInit ();
				break;
			}
		case WaveState.WaveIdle:
			{
				//temperary for testing/debugging
				if (forceSpawnWave) 
				{
					TriggerWave ();
					forceSpawnWave = false;
				}
				break;
			}
		}
	}


	public void TriggerWave()
	{
		m_State = WaveState.WaveSpawning;

		//calculate the timeing for the nodes
		CalculateTiming ();
	}

	//calculate times on when spawners will activate at which times.
	public void CalculateTiming()
	{
		EnemySpawnNode n1 = spawnersInOrder[0];

		//set timing for 1st node
		n1.timeStart = 0;
		n1.timeEnd = n1.timeStart + n1.spawner.TotalTimeForSpawner();

		//set timing for all other nodes past 1st node
		for (int i = 1; i < spawnersInOrder.Length; i++) 
		{
			EnemySpawnNode nPrev = spawnersInOrder[i-1];
			EnemySpawnNode n = spawnersInOrder[i];

			if(n.timeReferenceFromPreviousSpawn == TimeReference.AtStartOfSpawn)
			{
				n.timeStart = nPrev.timeStart + n.waitTimeFromPreviousSpawn;
			}
			else
			{
				n.timeStart = nPrev.timeEnd + n.waitTimeFromPreviousSpawn;
			}

			n.timeEnd = n.timeStart + n.spawner.TotalTimeForSpawner();
		}

		//find when the wave should end (when the last spawner is finished)
		for (int i = 0; i < spawnersInOrder.Length; i++) 
		{
			if(spawnersInOrder[i].timeEnd > m_TimeEnd)
			{
				m_TimeEnd = spawnersInOrder[i].timeEnd;
			}
		}

	}

}
