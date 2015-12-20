using UnityEngine;
using System.Collections;

public abstract class KillableObject : MonoBehaviour {

    public GameObject projectile;
    //public float fireRate;

    [HideInInspector]
    public Camera mainCamera;
    [HideInInspector]
    public GameObject killableObject;

    public virtual void CreateProjectile()
    {
        GameObject projectileClone = (GameObject)Instantiate(projectile, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);
        projectileClone.transform.SetParent(this.transform.parent);
    }
    
    //public virtual void FireWeapons()
    //{
    //    if (fireRate <= 0)
    //    {
    //        CreateProjectile();
    //        fireRate = 2f;
    //    }
    //    fireRate -= Time.deltaTime;
    //}
}
