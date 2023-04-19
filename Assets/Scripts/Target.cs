using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    public ParticleSystem explosionParticle;

    [SerializeField] float minSpeed = 12;
    [SerializeField] float maxSpeed = 16;
    [SerializeField] float maxTorque = 10;
    [SerializeField] float xRange = 4;
    [SerializeField] float ySpawnPos = -2;

    private Rigidbody targetRb;
    private GameManager gameManager;
   
    // Start is called before the first frame update
    void Start()
    {
        transform.position = RandomSpawnPos();
        
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) && gameManager.isGameActive && !gameManager.isGamePaused)
        {
            gameManager.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(1);
        }
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
}
