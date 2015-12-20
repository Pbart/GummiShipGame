using UnityEngine;
using System.Collections;



//SaberToothSpawner is for spawning sabertooths and making them follow a path (bezier spline). The amount of enemies and time in between
// each enemy is controlled in the inspector.
public class SaberToothSpawner : ISpawner
{
	public GameObject saberToothPrefab;				//The saber tooth prefab to spawn
	public BezierSplineFollower spawnPathScript;  	//The path each enemy will follow when spawned. They are spawned at the beginning of the path.
											// 10-21-15 - Eric - For right now, they will only follow a BezierSpline script. If there is no 
											// BezierSpline script, the enemy will spawn at the parent's transform's origin only and sit there.

	public uint spawnAmount;        //Total amount of enemies spawned
	public float spawnRate;         //Time between each enemy spawned
	public GameObject spawnParent;  //The parent to attach the enemies too. (usually either world or in player space)

	public bool forceSpawn;			//temprorary for testing/debugging


	private float m_fTimeElapsed;	//time elapsed since last spawn
	private uint m_iAmountspawned;	//enemies spawned since state changed to SpawnerSpawning

	// Use this for initialization
	void Start ()
	{
		SpawnerInit();
	}

	void SpawnerInit()
	{
		m_State = SpawnerState.SpawnerIdle;
		m_iAmountspawned = 0;
		m_fTimeElapsed = 0.0f;
	}

	void Update()
	{
		switch (m_State)
		{
			case SpawnerState.SpawnerSpawning:
				{
					//if there are still enemies to spawn
					if(m_iAmountspawned < spawnAmount)
					{
						//add time to time elapsed
						m_fTimeElapsed += Time.deltaTime;

						//spawn an enemy if its time (also used to spawn the first enemy on the same frame as when it was triggered)
						if(m_fTimeElapsed >= spawnRate || m_iAmountspawned == 0)
						{
							SpawnEnemy();
						}
					}
					//stop spawn if amount is spawned
					else if(m_iAmountspawned >= spawnAmount)
					{
						SpawnerInit();
					}
					break;
				}
			case SpawnerState.SpawnerFinished:
				{
					SpawnerInit();
					break;
				}
			case SpawnerState.SpawnerIdle:
				{
					//temperary for testing/debugging
					if (forceSpawn)
					{
						TriggerSpawn();
						forceSpawn = false;
					}
					break;
				}
			default:
				break;
		}
	}


	private void SpawnEnemy()
	{
		GameObject enemy = null;

		if(spawnPathScript)
		{
			enemy = (GameObject)Instantiate(saberToothPrefab, spawnPathScript.b_spline.transform.position, Quaternion.identity);

//			enemy.transform.position = spawnPath.transform.position;

			//set the enemy's bezierSpline to the spawnPaths
			BezierSplineFollower enemyFollowScript = enemy.GetComponent<BezierSplineFollower>();

			if (enemyFollowScript)
			{
				//TODO: create a copy constructore and assignment operator overlead so this is cleaner.
				enemyFollowScript.b_spline = spawnPathScript.b_spline;
				enemyFollowScript.duration = spawnPathScript.duration;
				enemyFollowScript.lookForward = spawnPathScript.lookForward;
				enemyFollowScript.mode = spawnPathScript.mode;
			}
		}
		else
		{
			enemy = (GameObject)Instantiate(saberToothPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		}

		if (spawnParent)
			enemy.transform.SetParent(spawnParent.transform);

		m_iAmountspawned++;
		m_fTimeElapsed = 0.0f;
	}


	public override void TriggerSpawn()
	{
		m_State = SpawnerState.SpawnerSpawning;
	}

	public override float TotalTimeForSpawner ()
	{
		return spawnRate * (float)(spawnAmount - 1);
	}


}
