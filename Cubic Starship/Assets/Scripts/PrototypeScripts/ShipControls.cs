using UnityEngine;
using System.Collections;

public class ShipControls : MonoBehaviour
{
    public GameObject projectile;
    public float fireRate = 0;
    public float shipMovementSpeed = 0.01f;

    private GameObject playerShip;
    private Vector3 viewportPos;
    //private Vector3 moveDirection;
    

    // Use this for initialization
    void Start()
    {
        playerShip = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetAxisInput();
        KeepPlayerInBounds();
        FireShipsWeapon();
        //playerShip.transform.position += moveDirection * shipMovementSpeed * Time.deltaTime;
    }
    /// <summary>
    /// Used to grab input values from the input manager
    /// </summary>
    private void GetAxisInput()
    {
        MoveHorizontal();
        MoveVertical();
    }

    /// <summary>
    /// Used to get the up vector of the camera and add it to the position of the player so that we are constantly staying infront of the camera
    /// </summary>
    void MoveVertical()
    {
        Vector3 cameraUpVector = Camera.main.transform.up;
        playerShip.transform.position += cameraUpVector * (Input.GetAxis("ShipVerticalMovement") * shipMovementSpeed);
    }

    /// <summary>
    /// Used to get the right vector of the camera and add it to the position of the player so that we are constantly staying infront of the camera
    /// </summary>
    void MoveHorizontal()
    {
        Vector3 cameraRightVector = Camera.main.transform.right;
        playerShip.transform.position += cameraRightVector * (Input.GetAxis("ShipHorizontalMovement") * shipMovementSpeed);
    }
    /// <summary>
    /// Used to keep the player in the camera screen bounds
    /// </summary>
    private void KeepPlayerInBounds()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position); //convert the player's position from world space to viewport space
        viewportPos.x = Mathf.Clamp01(viewportPos.x); //clamp the x value of the players position
        viewportPos.y = Mathf.Clamp01(viewportPos.y); //clamp the y value of the players position
        transform.position = Camera.main.ViewportToWorldPoint(viewportPos); //convert the player's position back to world space from viewport space
    }

    /// <summary>
    /// [TESTING METHOD]: Used to spawn projectiles and set them as a child of the ship so that they move with the ship as it moves
    /// </summary>
    void BulletCreation()
    {
        GameObject projectileClone = (GameObject)Instantiate(projectile, this.transform.position + new Vector3(0,-0.5f,0), transform.rotation);
        projectileClone.transform.SetParent(this.transform.parent);
    }
    /// <summary>
    /// [TESTING METHOD]: Used to fire projectiles from the ship's weapons at a set rate of fire (0.1 as of now). 
    /// Going to rewrite to called the weapon's fire method
    /// </summary>
    void FireShipsWeapon()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            if (fireRate <= 0)
            {
                BulletCreation();
                fireRate = 0.05f;
            }
            fireRate -= Time.deltaTime;
        }
    }
    
}
