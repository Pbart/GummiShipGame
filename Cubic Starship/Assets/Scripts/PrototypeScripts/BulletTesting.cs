using UnityEngine;
using System.Collections;

public class BulletTesting : MonoBehaviour
{
    private Vector3 viewportPos;
    private Vector3 directionVector;
    
    public float projectileSpeed;
    public float projectileLifetime;

    // Use this for initialization
    void Start ()
    {
        viewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
        directionVector = new Vector3(0, 0, projectileSpeed);
    }
	
	// Update is called once per frame
	void Update ()
    {
        BulletMovement();
        DestroySelf();
	}

    /// <summary>
    /// used to covert the bullets position into viewport space, move it forward, then convert it back to world space
    /// </summary>
    void BulletMovement()
    {
        viewportPos = Camera.main.WorldToViewportPoint(this.transform.position);
        viewportPos += directionVector;
        this.transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }

    void DestroySelf()
    {
        Destroy(gameObject, projectileLifetime);
    }

    
}
