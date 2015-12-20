using UnityEngine;
using System.Collections;

public class Garage : MonoBehaviour
{
    public GameObject[] shipList;
    public GameObject targets;
    public Vector3 spawnPoint;
    public BezierSplineFollower bsf;

    private int currentShip;
    private GameObject Ship;
    private GameObject temp;

	// Use this for initialization
	void Start ()
    {
        bsf = GameObject.Find("RotationObject").GetComponent<BezierSplineFollower>();
        bsf.enabled = false;
        spawnPoint = transform.position + new Vector3(0,0,10);
        currentShip = 0;
        InitialSpawn();
	}
	
	// Update is called once per frame
	void Update ()
    {
        ShowNext();
	}
    void InitialSpawn()
    {
        shipList = Resources.LoadAll<GameObject>("Prefabs/Ships/Temp");
        Ship = shipList[currentShip];
        temp = (GameObject)Instantiate(Ship,spawnPoint,Quaternion.identity);
    }

    void ShowNext()
    {
        int maxListSize = shipList.Length - 1;
        if(Input.GetKeyDown(KeyCode.D))
        {
            Destroy(temp);
            if(currentShip < maxListSize)
            currentShip++;
            Ship = shipList[currentShip];
            temp = (GameObject)Instantiate(Ship, spawnPoint, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Destroy(temp);
            if(currentShip > 0)
            currentShip--;
            Ship = shipList[currentShip];
            temp = (GameObject)Instantiate(Ship, spawnPoint, Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            temp.transform.parent = Camera.main.transform;
            temp.AddComponent<PlayerControls>();
            //bsf.enabled = true;
            PlayerControls pc = temp.GetComponent<PlayerControls>();
            pc.projectile = (GameObject)Resources.Load("Prefabs/Projectiles/Player Bullet");
            temp.tag = "Player";
            //targets.SetActive(true);
            enabled = false;
        }
    }
 
}
