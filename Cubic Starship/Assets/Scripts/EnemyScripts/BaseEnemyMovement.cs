using UnityEngine;
using System.Collections;

public enum PathMovementState
{
	IDLE,
	FOLLOWING_STARTING_PATH,
	FINISHED_STARTING_PATH,
	ERROR //in error state, no movement logic occurs.
}

//This class encompasses movement when the enemy first spawns.
//When the enemy first spawns, if there is a startingPath, it will follow that path before any other movement logic
public class BaseEnemyMovement : MonoBehaviour 
{
	private PathMovementState m_State = PathMovementState.IDLE;
	private BezierSplineFollower m_PathFollowScript;
	protected BaseEnemyManager manager;

	public virtual void Awake () 
	{
		m_PathFollowScript = GetComponent<BezierSplineFollower>();

		//disable path follow script until the manager says to start moving on the starting path
		if(m_PathFollowScript)
		{
			m_PathFollowScript.enabled = false;
		}
		else
		{
			print ("Error in " + this.gameObject.name + ": no path following script found.");
			m_State = PathMovementState.ERROR;
		}

		manager = GetComponent<BaseEnemyManager>();

	}

	//Called by this enemy's manager. Call this to start moving along the starting path and then into the spcific enemy movement logic.
	public void StartMoving()
	{

		if(m_State == PathMovementState.IDLE)
		{
			//if the b spline exists, that is the starting path. Start moving on it.
			if(m_PathFollowScript.b_spline)
			{
				m_PathFollowScript.enabled = true;
				m_State = PathMovementState.FOLLOWING_STARTING_PATH;
			}
			else //there is no starting path.
			{
				m_State = PathMovementState.FINISHED_STARTING_PATH;
			}
		}
	}

	public void StopMoving()
	{
		if(m_State != PathMovementState.ERROR)
		{
			m_State = PathMovementState.IDLE;
		}
	}

	//Eric - The update is not virtual because the starting path needs to be followed (which is what this base class does) before any enemy specific logic occurs.
	// Use MovementUpdate() when making specific logic derived from this class.
	virtual public void Update () 
	{
		//print (this.name + " BASE update called.");
		switch(m_State)
		{
		case PathMovementState.IDLE:
		{
			//intentionally empty. Awaiting manager to call StartMoving();
			break;
		}
		case PathMovementState.FOLLOWING_STARTING_PATH:
		{
			//check to see if the starting path is finished.
			if(m_PathFollowScript.IsFinished())
			{
				//disable path following to stop following the starting path
				m_PathFollowScript.enabled = false;
				m_State = PathMovementState.FINISHED_STARTING_PATH;
			}

			break;
		}
		case PathMovementState.FINISHED_STARTING_PATH:
		{
			MovementUpdate();
			break;
		}
		default:
		{
			//intentionally empty
			break;
		}
		}
	}

	virtual public void MovementUpdate()
	{
		//intentionally empty
	}
}
