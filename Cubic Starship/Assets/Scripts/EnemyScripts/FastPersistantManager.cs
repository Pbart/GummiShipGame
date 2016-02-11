using UnityEngine;
using System.Collections;

public enum FastPersistantState
{
	STATIONARY,
	PIVOTING,
	MOVING,
}


public class FastPersistantManager : BaseEnemyManager 
{
	private FastPersistantState state;


	// Use this for initialization
	public override void Awake () 
	{
		base.Awake ();
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		if(baseState == BaseEnemyState.ALIVE)
		{
			switch(state)
			{
			case FastPersistantState.STATIONARY:
			{

				break;
			}
			case FastPersistantState.PIVOTING:
			{
				
				break;
			}
			case FastPersistantState.MOVING:
			{
				
				break;
			}
			}
		}


		base.Update();
	}
}
