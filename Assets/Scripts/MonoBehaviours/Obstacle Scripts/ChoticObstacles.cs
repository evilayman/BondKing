using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoticObstacles : MonoBehaviour
{
    [SerializeField]
    private Vector3 randomLimit;
    private Vector3 spawnLocation, ObstacleDir;
    [SerializeField]
    private List<GameObject> chaoticObstacles;
    [SerializeField]
    private GameObject player;
    private GameObject instantiatedObstacle;
    [SerializeField]
    private float SpawnTime, destroyTime, chaoticForce;
    private int objectToSpawn, randRes;
    [SerializeField]
    public bool chaoticRandomGenerationMode;
    void Start ()
    {
        InvokeRepeating("SpawnObstacles", 0, SpawnTime);
    }
	
    void SpawnObstacles()
    {
        if (chaoticRandomGenerationMode)
        {
            var rand1 = Random.Range(-randomLimit.x, randomLimit.x);
            spawnLocation.x = rand1;
            spawnLocation.y = Random.Range(0, randomLimit.y);
            randRes = Random.Range(0, 2);
            if (randRes == 0)
                spawnLocation.z = randomLimit.z;
            else
                spawnLocation.z = -randomLimit.z;
            objectToSpawn = Random.Range(0, chaoticObstacles.Count);
            instantiatedObstacle = Instantiate(chaoticObstacles[objectToSpawn], spawnLocation, Quaternion.identity);
            ObstacleDir = player.transform.position - instantiatedObstacle.transform.position;
            instantiatedObstacle.GetComponent<Rigidbody>().AddForce(ObstacleDir * chaoticForce, ForceMode.Force);
            Destroy(instantiatedObstacle, destroyTime);
        }
    }
}
