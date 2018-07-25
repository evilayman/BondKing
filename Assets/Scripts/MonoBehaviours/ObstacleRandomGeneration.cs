using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Left, Right }
public class ObstacleRandomGeneration : MonoBehaviour
{

    [SerializeField]
    private Vector3 randomLimit;
    private Vector3 spawnLocation, ObstacleDir;
    [SerializeField]
    private List<GameObject> chaoticObstacles, normalObstacles;
    [SerializeField]
    private GameObject player;
    private GameObject instantiatedObstacle;
    [SerializeField]
    private float SpawnTime, destroyTime, followPlayerSpeed,goTo,step;
    private int objectToSpawn, randRes;
    [SerializeField]
    private bool chaoticRandomGenerationMode;
    private Direction dir;

    private void Start()
    {
        InvokeRepeating("SpawnObstacles", 0, SpawnTime);
    }
    void Update()
    {

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
            instantiatedObstacle.GetComponent<Rigidbody>().AddForce(ObstacleDir * followPlayerSpeed);
            //instantiatedObstacle.GetComponent<Rigidbody>().MovePosition(player.transform.position*2);
            Destroy(instantiatedObstacle, destroyTime);
        }
        else
        {
            objectToSpawn = Random.Range(0, normalObstacles.Count);
            dir = (Direction)Random.Range(0, 4);
            switch (dir)
            {
                case Direction.Up:
                    instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(0, 1, 40), Quaternion.identity);
                    StartCoroutine(MoveObstacle(instantiatedObstacle, new Vector3(0, 0, goTo)));
                    //instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -followPlayerSpeed));

                    break;
                case Direction.Down:
                    instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(0, 1, -40), Quaternion.identity);
                    StartCoroutine(MoveObstacle(instantiatedObstacle, new Vector3(0, 0, -goTo)));
                    //instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, followPlayerSpeed));
                    break;
                case Direction.Left:
                    instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(-40, 1, 0), Quaternion.identity);
                    instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                    StartCoroutine(MoveObstacle(instantiatedObstacle, new Vector3(-goTo, 0, 0)));
                    //instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(followPlayerSpeed, 0, 0));
                    break;
                case Direction.Right:
                    instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(40, 1, 0), Quaternion.identity);
                    instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                    StartCoroutine(MoveObstacle(instantiatedObstacle, new Vector3(goTo, 0, 0)));
                    //instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(-followPlayerSpeed, 0, 0));
                    break;
                default:
                    break;
            }
            Destroy(instantiatedObstacle, destroyTime);
        }

    }

    IEnumerator MoveObstacle(GameObject obstacle, Vector3 dir)
    {
        float timeToStart = Time.time;
        Vector3 moveStep = dir.normalized;
        while (obstacle)
        {
            obstacle.GetComponent<Rigidbody>().MovePosition(dir -  moveStep);
            if (moveStep.x > 0)
            {
                moveStep.x+= step;
            }
            else if (moveStep.x < 0)
            {
                moveStep.x-=step;
            }
            if (moveStep.z > 0)
            {
                moveStep.z+= step;
            }
            else if (moveStep.z < 0)
            {
                moveStep.z-= step;
            }

            yield return null;// new WaitForSeconds(0.1f);
        }
    }
}
