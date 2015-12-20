using UnityEngine;
using System.Collections;


public class suicideShip : MonoBehaviour {
    Vector3 targetLoctaion;
    private Transform player;
    float timeTaken = 3f;
    float distanceToTravel = 10f;
    bool isLerping = false;
    public float percentageComplete;
    Vector3 startpos;
    float timeStarted;
    float explosionRadius = 2f;
    float explosionPower = 5f;
    public Collider[] colliders;
    public GameObject explosionParticle;
    private EnemyFireScript efs;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        efs = gameObject.GetComponent<EnemyFireScript>();
        this.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        startpos = transform.position;
        targetLoctaion = player.transform.position;
        timeStarted = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
    void lerp()
    {
        isLerping = true;
        
        
        
    }
    void FixedUpdate()
    {
        float deltaTime = Time.time - timeStarted;
        percentageComplete = deltaTime / timeTaken;
        transform.position = Vector3.Lerp(startpos, targetLoctaion, percentageComplete);
        if (percentageComplete >= 1f)
        {
           
            KillYourself();
        }
    }
    void KillYourself()
    {
        bool hitPlayer = false;
        Vector3 explosion = transform.position;
        colliders = Physics.OverlapSphere(explosion, explosionRadius);
       
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionPower, explosion, explosionRadius);
                hitPlayer = true;
            }
        }
        if (hitPlayer)
        {
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
