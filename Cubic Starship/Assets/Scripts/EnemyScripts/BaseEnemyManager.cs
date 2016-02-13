using UnityEngine;
using System.Collections;

public enum BaseEnemyState
{
	SPAWNING,
	ALIVE,
	KILLED,			
	DESPAWNING,
	ERROR
}

public class BaseEnemyManager : MonoBehaviour 
{
	public BaseEnemyState baseState{get; set;}
	public BaseEnemyMovement baseMovement;
	public int scoreValue;

	public bool IsAlive(){return (baseState == BaseEnemyState.SPAWNING) || (baseState == BaseEnemyState.ALIVE);}

	public virtual void Awake()
	{		
		baseState = BaseEnemyState.SPAWNING;
	}

	public virtual void Kill()
	{
		if(baseState == BaseEnemyState.ALIVE)
		{
			baseState = BaseEnemyState.KILLED;
			baseMovement.StopMoving();

			//add score pickups here
		}
	}

	public virtual void Despawn()
	{
		if(baseState == BaseEnemyState.ALIVE)
		{
			baseState = BaseEnemyState.DESPAWNING;
			baseMovement.StopMoving();
		}

	}

	// Update is called once per frame
	public virtual void Update () 
	{
		switch(baseState)
		{
		//Eric 2/10/16 - for right now, the enemy changes state from spawning to alive instantly. This will probably change when/if we get animations showing the enemy spawning.
		case BaseEnemyState.SPAWNING:
		{
			baseMovement.StartMoving();
			baseState = BaseEnemyState.ALIVE;
			break;
		}
		case BaseEnemyState.KILLED:		//temporarily just get instantly rid of them, This line will most likely not be here in the future (ned to play exiting animations or particle effects)
		{
			Destroy(this.gameObject);
			break;
		}
		case BaseEnemyState.DESPAWNING:	//temporarily just get instantly rid of them, This line will most likely not be here in the future (ned to play exiting animations or particle effects)
		{
			Destroy (this.gameObject);
			break;
		}
		}
	}
}
