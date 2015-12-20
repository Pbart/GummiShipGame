using UnityEngine;
using System.Collections;

public class SingleDirectionalCannon : MonoBehaviour {

    public float fireRate; //how fast the weapon should fire
    public float projectileSpeed = 1f; //how fast the projectile spawn should move

    public GameObject projectile; //what projectile to spawn when shooting

    private float hiddenFireRate; //internal cooldown for firing

    private Vector3 forwardVector; //used to determine where the forward of the bullet should be
    private Vector3 directionVector; //used to tell what direction the bullet should be fired in

    private GameObject projectileClone; //the projectile we will be shooting

    // Use this for initialization
    void Start()
    {
        hiddenFireRate = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        forwardVector = this.transform.position - Camera.main.transform.position;
        directionVector = NormalizedForwardVector(forwardVector) * projectileSpeed;
    }

    /// <summary>
    /// Used to get the normalized forward vector 
    /// </summary>
    /// <param name="direction">Direction that needs to be normalized</param>
    /// <returns></returns>
    public virtual Vector3 NormalizedForwardVector(Vector3 direction)
    {
        return direction.normalized;
    }

    /// <summary>
    /// Used to create a projectile, set it's parent and tell it what direction to travel to
    /// </summary>
    public virtual void CreateProjectile()
    {
        projectileClone = (GameObject)Instantiate(projectile, transform.position + new Vector3(0f, -0.5f, 0), transform.rotation);
        projectileClone.transform.SetParent(Camera.main.transform);
        projectileClone.GetComponent<PlayerBullet>().DirectionVector = directionVector;
    }

    /// <summary>
    /// Used to fire the weapon at the determined rate of fire
    /// </summary>
    public virtual void FireWeapons()
    {
        if (fireRate <= 0)
        {
            CreateProjectile();
            fireRate = hiddenFireRate;
        }
        fireRate -= Time.deltaTime;
    }

    #region Properties
    public float HiddenFireRate
    {
        get { return hiddenFireRate; }
        set { hiddenFireRate = value; }
    }

    public Vector3 ForwardVector
    {
        get { return forwardVector; }
        set { forwardVector = value; }
    }
    #endregion
}

