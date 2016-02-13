using UnityEngine;
using System.Collections;

//The BlizzardType Weapon is a rapid fire, mutli-direction cannon that depending on the level of the cannon, 
//shoots anywhere between 2 and 4 directions
public class MultiDirectionalCannon : SingleDirectionalCannon
{
    //Vectors to tell the spawning projectile where to go
    private Vector3 directionVector1;
    private Vector3 directionVector2;
    private Vector3 directionVector3;
    private Vector3 directionVector4;

    //Vectors used to help get what direction the projectile need to go
    private Vector3 offsetVector1;
    private Vector3 offsetVector2;

    //The projectiles spawned by the weapon
    private GameObject projectileClone1;
    private GameObject projectileClone2;
    private GameObject projectileClone3;
    private GameObject projectileClone4;

    // Use this for initialization
    void Start()
    {
        if (Tier == WeaponTier.Tier1)
        {
            gameObject.name = "Bi-Directional Cannon";
        }
        else if (Tier == WeaponTier.Tier2)
        {
            gameObject.name = "Tri-Directional Cannon";
        }
        else
        {
            gameObject.name = "Quad-Directional Cannon";
        }
        HiddenFireRate = fireRate;
        offsetVector1 = new Vector3(3f, 0f, 0f);
        offsetVector2 = new Vector3(0f, 2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        ForwardVector = transform.position - Camera.main.transform.position;
        if (Tier == WeaponTier.Tier1)
        {
            directionVector1 = NormalizedForwardVector(ForwardVector, offsetVector1) * projectileSpeed;
            directionVector2 = NormalizedForwardVector(ForwardVector, -offsetVector1) * projectileSpeed;
        }
        if (Tier == WeaponTier.Tier2 || Tier == WeaponTier.Tier3)
        {
            directionVector1 = NormalizedForwardVector(ForwardVector, offsetVector1, offsetVector2) * projectileSpeed;
            directionVector2 = NormalizedForwardVector(ForwardVector, -offsetVector1, offsetVector2) * projectileSpeed;
            directionVector3 = NormalizedForwardVector(ForwardVector, -offsetVector2) * projectileSpeed;
            if (Tier == WeaponTier.Tier3)
            {
                directionVector3 = NormalizedForwardVector(ForwardVector, offsetVector1, -offsetVector2) * projectileSpeed;
                directionVector4 = NormalizedForwardVector(ForwardVector, -offsetVector1, -offsetVector2) * projectileSpeed;
            }
        }

        ////Used for Debugging/Visualization purposes
        //Debug.DrawRay(transform.position, ForwardVector, Color.red);
        //Debug.DrawRay(transform.position, ForwardVector + offsetVector1, Color.green);
        //Debug.DrawRay(transform.position, ForwardVector - offsetVector1, Color.green);
    }

    /// <summary>
    /// Used to calculate the normalized direction of where the projectile should head towards
    /// </summary>
    private Vector3 NormalizedForwardVector(Vector3 direction, Vector3 offset)
    {
        Vector3 sum = direction + offset;
        return sum.normalized;
    }

    private Vector3 NormalizedForwardVector(Vector3 direction, Vector3 offset1, Vector3 offset2)
    {
        Vector3 sum = direction + offset1 + offset2;
        return sum.normalized;
    }

    /// <summary>
    /// This method is used to create a projectile, set the projectile's parent and tell the projectile what direction it needs to travel
    /// </summary>
    public override void CreateProjectile()
    {
        projectileClone1 = (GameObject)Instantiate(projectile, this.transform.position + new Vector3(0.5f, -0.5f, 0), transform.rotation); //* Quaternion.Euler(0,30f,0));
        projectileClone1.transform.SetParent(Camera.main.transform);
        projectileClone1.GetComponent<PlayerBullet>().DirectionVector = directionVector1;

        projectileClone2 = (GameObject)Instantiate(projectile, this.transform.position + new Vector3(-0.5f, -0.5f, 0), transform.rotation);
        projectileClone2.transform.SetParent(Camera.main.transform);
        projectileClone2.GetComponent<PlayerBullet>().DirectionVector = directionVector2;

        if (Tier == WeaponTier.Tier2 || Tier == WeaponTier.Tier3)
        {
            projectileClone3 = (GameObject)Instantiate(projectile, this.transform.position + new Vector3(0f, -0.5f, 0), transform.rotation);
            projectileClone3.transform.SetParent(Camera.main.transform);
            projectileClone3.GetComponent<PlayerBullet>().DirectionVector = directionVector3;

            if (Tier == WeaponTier.Tier3)
            {
                projectileClone3.transform.position = projectileClone3.transform.position + new Vector3(0.5f, 0f, 0f);
                projectileClone4 = (GameObject)Instantiate(projectile, this.transform.position + new Vector3(-0.5f, -0.5f, 0), transform.rotation);
                projectileClone4.transform.SetParent(Camera.main.transform);
                projectileClone4.GetComponent<PlayerBullet>().DirectionVector = directionVector4;
            }
        }
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
