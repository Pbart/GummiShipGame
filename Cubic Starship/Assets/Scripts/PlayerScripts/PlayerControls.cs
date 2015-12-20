using UnityEngine;
using System.Collections;

public class PlayerControls : KillableObject
{
    #region Fields/Attribute
    public float shipHorizontalMovementSpeed = 0.07f;
    public float shipVerticalMovementSpeed = 0.05f;

    private float shipMovementSpeedBoost = 1f;
    private float horizontalValue;
    private float verticalValue;

    private SingleDirectionalCannon[] weapons;
    private Vector3 viewportPos;
    private Animator anim;
    #endregion

    #region Methods
    // Use this for initialization
    void Start()
    {
        killableObject = this.gameObject;
        mainCamera = Camera.main;
        weapons = this.GetComponentsInChildren<SingleDirectionalCannon>();
        anim = gameObject.GetComponent<Animator>();

        for (int i = 0; i < weapons.Length; i++)
        {
            Debug.Log("Name of Weapon: " + weapons[i].gameObject.name + " Script Name: " + weapons[i].GetComponentInChildren<SingleDirectionalCannon>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetAxisInput();
        KeepPlayerInBounds();
        FireWeapons();
        DoDefensiveAction();

        //Debug.Log(Input.GetAxis("ShipHorizontalMovement"));
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
    private void MoveVertical()
    {
        verticalValue = Input.GetAxis("ShipVerticalMovement");
        Vector3 cameraUpVector = mainCamera.transform.up;
        killableObject.transform.position += cameraUpVector * (verticalValue * (shipVerticalMovementSpeed * shipMovementSpeedBoost));
    }

    /// <summary>
    /// Used to get the right vector of the camera and add it to the position of the player so that we are constantly staying infront of the camera
    /// </summary>
    private void MoveHorizontal()
    {
        horizontalValue = Input.GetAxis("ShipHorizontalMovement");
        if (horizontalValue != 0)
        {
            anim.SetBool("IsStationaryHorizontal", false);
        }
        else
        {
            anim.SetBool("IsStationaryHorizontal", true);
        }
        anim.SetFloat("Direction", horizontalValue);
        Vector3 cameraRightVector = mainCamera.transform.right;
        killableObject.transform.position += cameraRightVector * (horizontalValue * (shipHorizontalMovementSpeed * shipMovementSpeedBoost));
    }

    /// <summary>
    /// Used to keep the player in the camera screen bounds
    /// </summary>
    private void KeepPlayerInBounds()
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position); //convert the player's position from world space to viewport space
        viewportPos.x = Mathf.Clamp01(viewportPos.x); //clamp the x value of the players position
        viewportPos.y = Mathf.Clamp01(viewportPos.y); //clamp the y value of the players position
        transform.position = mainCamera.ViewportToWorldPoint(viewportPos); //convert the player's position back to world space from viewport space
    }
    
    /// <summary>
    /// [TESTING METHOD]: This will be updated to call the weapon's fire method
    /// </summary>
    public void FireWeapons()
    {
        if (Input.GetAxis("FireProjectile") == 1)
        {
            for (int w = 0; w < weapons.Length; w++)
            {
                weapons[w].FireWeapons();
            }
        }
    }
    
    /// <summary>
    /// [TESTING METHOD]: Used to have the ship perfrom thier defensive action like barrel rolling or put up a shield
    /// This will be moved to the ship component that handles what defensive action the player equips to the ship
    /// </summary>
    private void DoDefensiveAction()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shipMovementSpeedBoost = 2.5f;
            anim.SetTrigger("DefensiveButton");
        }
        else
        {
            shipMovementSpeedBoost = 1f;
        }
    }
    #endregion
}
