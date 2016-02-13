using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomingLazerCannon : SingleDirectionalCannon
{
    private int numProjectiles;
    private bool hasTargetsBeenAcquired = false;
    private bool isInScreen;
    
    public List<GameObject> allTargets;
    public GameObject[] enemies;
    private GameObject acquiredTarget;
    private Vector3 screenPos;

    public GameObject[] projectileClones;
    
    // Use this for initialization
    void Start ()
    {
        if (Tier == WeaponTier.Tier1)
        {
            numProjectiles = 2;
        }
        else if (Tier == WeaponTier.Tier2)
        {
            numProjectiles = 4;
        }
        else
        {
            numProjectiles = 6;
        }
        allTargets = new List<GameObject>();

        for (int i = 0; i < projectileClones.Length; i++)
        {
            projectileClones[i] = projectile;
            Debug.Log(projectileClones[i].name);
        }
        HiddenFireRate = fireRate;
        fireRate = 0;
        Debug.Log("The number of projectiles to fire is " + numProjectiles);
	}
	
	// Update is called once per frame
	void Update ()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //CheckBulletArray();
        CheckIfTargetsAreOnScreen(enemies);
        if (Input.GetKeyUp(KeyCode.X))
        {
            if (allTargets != null)
            {
                //AcquireTarget();
                //CheckIfTargetsAreOnScreen(enemies);
                CreateProjectile();
            }
        }
    }

    private void CheckBulletArray()
    {
        CheckIfTargetsAreOnScreen(enemies);
    }

    private void CheckIfTargetsAreOnScreen(GameObject[] targets)
    {
        int j = 0;
        allTargets.Clear();
        for (int i = 0; i < targets.Length; i++)
        {
            if (j > numProjectiles -1 || i > targets.Length-1)
            {
                break;
            }
            Vector3 screenPos = Camera.main.WorldToViewportPoint(targets[i].transform.position);
            if (screenPos.x >= 0 && screenPos.y >= 0 && screenPos.x < 1 && screenPos.y < 1)
            {
                allTargets.Add(targets[i]);
                j = j + 1;
            }
        }
        hasTargetsBeenAcquired = true;
    }

    public override void CreateProjectile()
    {
        projectileClones = new GameObject[allTargets.Count];
        if (allTargets == null)
        {
            Debug.Log("There are no targets to fire at");
            return;
        }
        for (int i = 0; i < allTargets.Count; i++)
        {
            projectileClones[i] = (GameObject)Instantiate(projectile, transform.position + new Vector3(0f, -0.5f, 0f), transform.rotation);
            projectileClones[i].transform.SetParent(Camera.main.transform);
            projectileClones[i].GetComponent<LazerBullet>().Target = allTargets[i].transform;
            projectileClones[i].GetComponent<LazerBullet>().ProjectileSpeed = projectileSpeed;
            Debug.Log(projectileClones[i].name + " target is " + allTargets[i].name);
        }
    }

    public override void FireWeapons()
    {
        //if (hasTargetsBeenAcquired == true)
        //{
        //    for (int i = 0; i < projectileClones.Length; i++)
        //    {
        //        if (projectileClones[i] == null)
        //        {
        //            CreateProjectile();
        //        }
        //    }
        //}
        if (fireRate <= 0)
        {
            CreateProjectile();
            fireRate = HiddenFireRate;
        }
        fireRate -= Time.deltaTime;
    }
}
