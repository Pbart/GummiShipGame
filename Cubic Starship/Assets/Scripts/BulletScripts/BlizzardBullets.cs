﻿using UnityEngine;
using System.Collections;

public class BlizzardBullets : MonoBehaviour
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

    /// <summary>
    /// used to covert the bullets position into viewport space, move it forward, then convert it back to world space
    /// </summary>
    void BulletMovement()
    {
        this.transform.position += DirectionVector;
    }

    void DestroySelf()
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
