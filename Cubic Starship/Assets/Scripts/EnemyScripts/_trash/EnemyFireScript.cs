using UnityEngine;
using System.Collections;

public class EnemyFireScript : KillableObject
{
    private Vector3 viewportPos;
    public EnemySpawner enemySpawner;
    public float respawnRate;
    private GameObject obj;
    managerScript ms;
    public bool spawn = false;
    void Start()
    {
        
        killableObject = this.gameObject;
        mainCamera = Camera.main;
        obj = GameObject.Find("Game Manager");
        ms = obj.GetComponent<managerScript>();
    }

    void Update()
    {
        viewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (viewportPos.x > 0 && viewportPos.y > 0 && viewportPos.x <= 1 && viewportPos.y <= 1)
        {            
            if(projectile != null)
            {
                //FireWeapons();
            }
        }
        FindPlayer();
    }
    
    void FindPlayer()
    {
        killableObject.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }

    public override void CreateProjectile()
    {
        GameObject clone = (GameObject)Instantiate(projectile, killableObject.transform.position, killableObject.transform.rotation);
        clone.transform.parent = (mainCamera.transform);
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Debug.Log(gameObject.name + "'s spawner is " + enemySpawner.name);
            enemySpawner.spawn = true;
            ms.updateScore();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            yield return null;
        }
    }
   
    IEnumerator delaySpawn()
    {
        
        ms.updateScore();
        enemySpawner.SpawnEnemy();
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        StopCoroutine(delaySpawn());
    }
}
