using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnedEnemy;
    public GameObject spawnParent;
    public float spawnRate;
    private float spawn_Rate;
    public bool spawn;
	// Use this for initialization
	void Start ()
    {
        spawn_Rate = spawnRate;
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnEnemy();
	}


    public void SpawnEnemy()
    {
        if (spawn == true)
        {
            Debug.Log("Spawn is true");
            spawnRate -= Time.deltaTime;
            if (spawnRate <= 0)
            {
                Debug.Log("Spawning " + spawnedEnemy.name + " at " + this.gameObject.name);
                spawnParent = GameObject.Find("Targets");
                GameObject clone = (GameObject)Instantiate(spawnedEnemy, this.transform.localPosition, Quaternion.identity);
                Debug.Log(this.gameObject.name + "'s spawn is " + spawn);
                spawn = !spawn;
                Debug.Log(this.gameObject.name + "'s spawn is now " + spawn);
                spawnRate = spawn_Rate;
                Debug.Log("spawnRate is " + spawnRate);
            }
        }
    }
}
