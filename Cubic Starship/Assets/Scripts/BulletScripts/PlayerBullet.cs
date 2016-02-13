using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{
    public float projectileLifetime;

    private Vector3 directionVector;
    private GameObject playerBullet;

    // Use this for initialization
    void Start()
    {
        playerBullet = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        BulletMovement();
        DestroySelf();
    }

    public virtual void BulletMovement()
    {
        this.transform.position += DirectionVector;
    }

    public virtual void DestroySelf()
    {
        Destroy(playerBullet, projectileLifetime);
    }

    //Properties
    public Vector3 DirectionVector
    {
        get { return directionVector; }
        set { directionVector = value; }
    }
}
