using UnityEngine;
using System.Collections;

public class EnemyProjectiles : MonoBehaviour
{
    public float projectileSpeed;
    public float projectileLifetime;

	// Update is called once per frame
	void Update ()
    {
        this.transform.position += transform.forward * projectileSpeed;
        DestroySelf();
	}

    void DestroySelf()
    {
        Destroy(gameObject, projectileLifetime);
    }
}
