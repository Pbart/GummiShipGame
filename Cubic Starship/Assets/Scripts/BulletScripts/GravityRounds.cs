using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class GravityRounds : PlayerBullet
{
    public int explosionRadius;
    private SphereCollider sCol;

    void Start()
    {
        sCol = this.GetComponent<SphereCollider>();
        sCol.radius = explosionRadius;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy Hit");
        }
    }
}
