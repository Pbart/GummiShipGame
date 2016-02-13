using UnityEngine;
using System.Collections;

public class TurnThisOffBeforeGameStarts : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		this.gameObject.SetActive(false);
	}
}
