using UnityEngine;
using System.Collections;

public class LazerBullet : MonoBehaviour
{
    private float projectileSpeed;
    private float projectileLifeTime = 5f;
    private float projectileDamping = 5f;

    private Transform target;
    private GameObject lazerObject;
    private Quaternion lookRot;

	// Use this for initialization
	void Start ()
    {
        lazerObject = this.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckIfTargetExists();
        HomeInOnTarget();
        DestroySelf();
    }

    private void CheckIfTargetExists()
    {
        if (target == null)
        {
            Destroy(lazerObject);
        }
    }

    private void HomeInOnTarget()
    {
        if(target != null)
        {
            this.transform.Translate(this.transform.forward * Time.deltaTime * projectileSpeed);
            lookRot = Quaternion.LookRotation(target.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRot, Time.deltaTime * projectileDamping);
        }
    }

    private void DestroySelf()
    {
        Destroy(lazerObject, projectileLifeTime);
    }

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }

    public Transform Target
    {
        get { return Target;}
        set { target = value; }
    }
}
