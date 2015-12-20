using UnityEngine;
using System.Collections;

public class TestingCameraSpaces : MonoBehaviour
{
    public GameObject projectile;
    public float fireRate = 0f;

    void Update()
    {
        FindTarget();
        ShootAtPlayer();
    }
    /// <summary>
    /// Have the enemy look at the player
    /// </summary>
    void FindTarget()
    {
        this.transform.LookAt(GameObject.Find("Tortoise").transform);
    }

    void SpawnProjectile()
    {
        GameObject clone = (GameObject)Instantiate(projectile, this.transform.position, this.transform.rotation);
        clone.transform.parent = (Camera.main.transform);
    }

    /// <summary>
    /// Have the enemy spawn a projectile every 2 seconds
    /// </summary>
    void ShootAtPlayer()
    {
        if (fireRate <= 0)
        {
            SpawnProjectile();
            fireRate = 2f;
        }
        fireRate -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Debug.Log("HIT!!!");
            Destroy(gameObject);
        }
    }   
}
