using UnityEngine;
using System.Collections;

public class AOEBullet : PlayerBullet
{
    public float explosionRadius;

    //private Vector3 directionVector;
    private GameObject[] enemies;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletMovement();
        DestroySelf();
    }

    /// <summary>
    /// used to covert the bullets position into viewport space, move it forward, then convert it back to world space
    /// </summary>
    public override void BulletMovement()
    {
        base.BulletMovement();
    }

    private void FindEnemiesInExplosionRadius(Collider targetHit)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector3.Distance(targetHit.transform.position, enemies[i].transform.position);
            //Debug.Log("Distance between " + targetHit.name + " and " + enemies[i] + " is " + distance);
            if (distance <= explosionRadius)
            {
                Debug.Log(enemies[i] + " is in the explosion radius");
                Destroy(enemies[i]);
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            FindEnemiesInExplosionRadius(other);
        }
    }
    public override void DestroySelf()
    {
        Destroy(this.gameObject, projectileLifetime);
    }

    public float ExplosionRadius
    {
        get { return explosionRadius; }
        set { explosionRadius = value; }
    }
}
