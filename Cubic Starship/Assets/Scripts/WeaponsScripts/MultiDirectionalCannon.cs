using UnityEngine;
using System.Collections;

//The BlizzardType Weapon is a rapid fire, mutli-direction cannon that depending on the level of the cannon, 
//shoots anywhere between 2 and 4 directions
public class MultiDirectionalCannon : SingleDirectionalCannon
{
    //Vectors to tell the spawning projectile where to go
    private Vector3 directionVector1;
    private Vector3 directionVector2;

    //Vectors used to help get what direction the projectile need to go
    private Vector3 offsetVector;

    //The projectiles spawned by the weapon
    private GameObject projectileClone1;
    private GameObject projectileClone2;

    // Use this for initialization
    void Start()
    {
        HiddenFireRate = fireRate;
        offsetVector = new Vector3(3f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        ForwardVector = transform.position - Camera.main.transform.position;

        directionVector1 = NormalizedForwardVector(ForwardVector, offsetVector) * projectileSpeed;
        directionVector2 = NormalizedForwardVector(ForwardVector, -offsetVector) * projectileSpeed;

        //Used for debugging purposes
        Debug.DrawRay(transform.position, ForwardVector, Color.red);
        Debug.DrawRay(transform.position, ForwardVector + offsetVector, Color.green);
        Debug.DrawRay(transform.position, ForwardVector - offsetVector, Color.green);
    }

    /// <summary>
    /// Used to calculate the normalized direction of where the projectile should head towards
    /// </summary>
    private Vector3 NormalizedForwardVector(Vector3 direction, Vector3 offset)
    {
        Vector3 sum = direction + offset;
        return sum.normalized;
    }

    /// <summary>
    /// This method is used to create a projectile, set the projectile's parent and tell the projectile what direction it needs to travel
    /// </summary>
    public override void CreateProjectile()
    {
        projectileClone1 = (GameObject)Instantiate(projectile, this.transform.position + new Vector3(0.5f, -0.5f, 0), transform.rotation); //* Quaternion.Euler(0,30f,0));
        projectileClone1.transform.SetParent(Camera.main.transform);
        projectileClone1.GetComponent<BlizzardBullets>().DirectionVector = directionVector1;

        projectileClone2 = (GameObject)Instantiate(projectile, this.transform.position + new Vector3(-0.5f, -0.5f, 0), transform.rotation);
        projectileClone2.transform.SetParent(Camera.main.transform);
        projectileClone2.GetComponent<BlizzardBullets>().DirectionVector = directionVector2;
    }

    /// <summary>
    /// This is used to fire the weapons and determine the rate of fire of the weapon 
    /// </summary>
    public override void FireWeapons()
    {
        if (fireRate <= 0)
        {
            CreateProjectile();
            fireRate = HiddenFireRate;
        }
        fireRate -= Time.deltaTime;
    }
}
