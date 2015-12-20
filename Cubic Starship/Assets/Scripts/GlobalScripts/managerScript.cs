using UnityEngine;
using System.Collections;

public class managerScript : MonoBehaviour {
   public float score = 0;
	// Use this for initialization
	void Start () {
	
	}
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
	// Update is called once per frame
	public void updateScore () {
        score++;
        if(score == 11)
        { 
            //Application.LoadLevel(0);
            Application.LoadLevel(0);
        }
	
	}
}
