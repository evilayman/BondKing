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
    private float SpawnTime, destroyTime, chaoticForce, organizedForce/*,step*/;
    private int objectToSpawn, randRes, upObsCnt, downObsCnt, leftObsCnt, rightObsCnt;
    [SerializeField]
    private bool chaoticRandomGenerationMode;
    private Direction dir;
    private bool canSpawnUp, canSpawnDown, canSpawnLeft, canSpawnRight, randomFailed;
    private float upDownTime, leftRightTime;

    private void Start()
    {
        randomFailed = true;
        canSpawnUp = canSpawnDown = canSpawnLeft = canSpawnRight = true;
        upObsCnt = downObsCnt = leftObsCnt = rightObsCnt = 0;
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
            instantiatedObstacle.GetComponent<Rigidbody>().AddForce(ObstacleDir * chaoticForce, ForceMode.Acceleration);
            Destroy(instantiatedObstacle, destroyTime);
        }
        else
        {
            if (!canSpawnUp)
            {
                if (Time.time - upDownTime >= destroyTime)
                {
                    canSpawnUp = true;
                    downObsCnt = 0;
                    print("Can Spawn Up Time" + (Time.time - upDownTime));
                }
            }
            if (!canSpawnDown)
            {
                if (Time.time - upDownTime >= destroyTime)
                {
                    canSpawnDown = true;
                    upObsCnt = 0;
                    print("Can Spawn Down Time" + (Time.time - upDownTime));
                }
            }
            if (!canSpawnLeft)
            {
                if (Time.time - leftRightTime >= destroyTime)
                {
                    canSpawnLeft = true;
                    rightObsCnt = 0;
                }
            }
            if (!canSpawnRight)
            {
                if (Time.time - leftRightTime >= destroyTime)
                {
                    leftObsCnt = 0;
                    canSpawnRight = true;
                }
            }
            objectToSpawn = Random.Range(0, normalObstacles.Count);
            while (randomFailed)
            {
                dir = (Direction)Random.Range(0, 4);
                switch (dir)
                {
                    case Direction.Up:
                        if (canSpawnUp)
                        {
                            randomFailed=false;
                        }
                        break;
                    case Direction.Down:
                        if (canSpawnDown)
                        {
                            randomFailed = false;
                        }
                        break;
                    case Direction.Left:
                        if (canSpawnLeft)
                        {
                            randomFailed = false;
                        }
                        break;
                    case Direction.Right:
                        if (canSpawnRight)
                        {
                            randomFailed = false;
                        }
                        break;
                    default:
                        break;
                }
            }
            randomFailed = true;
            switch (dir)
            {
                case Direction.Up:

                    if ( upObsCnt < 3 && canSpawnUp)
                    {
                        canSpawnDown = false;
                        upObsCnt++;
                        instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(0, 0, 40), Quaternion.identity);
                        instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -organizedForce), ForceMode.Acceleration);
                        upDownTime = Time.time;
                        Destroy(instantiatedObstacle, destroyTime);
                    }
                   

                    break;

                case Direction.Down:
                    if (downObsCnt < 3 && canSpawnDown)
                    {
                        downObsCnt++;
                        instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(0, 0, -40), Quaternion.identity);
                        instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, organizedForce), ForceMode.Acceleration);
                        canSpawnUp = false;
                        upDownTime = Time.time;
                        Destroy(instantiatedObstacle, destroyTime);
                    }
                
                    break;

                case Direction.Left:
                    if (leftObsCnt < 3 && canSpawnLeft)
                    {
                        leftObsCnt++;
                        instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(-40, 0, 0), Quaternion.identity);
                        instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                        instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(organizedForce, 0, 0), ForceMode.Acceleration);
                        canSpawnRight = false;
                        leftRightTime = Time.time;
                        Destroy(instantiatedObstacle, destroyTime);
                    }

                    break;

                case Direction.Right:
                    if ( leftObsCnt < 3 && canSpawnRight)
                    {
                        rightObsCnt++;
                        instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(40, 0, 0), Quaternion.identity);
                        instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                        instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(-organizedForce, 0, 0), ForceMode.Acceleration);
                        canSpawnLeft = false;
                        leftRightTime = Time.time;
                        Destroy(instantiatedObstacle, destroyTime);
                    }

                    break;
                default:
                    break;
            }

        }

    }

    //IEnumerator MoveObstacle(GameObject obstacle, Vector3 dir)
    //{
    //    Vector3 moveStep = dir.normalized;
    //    while (obstacle)
    //    {
    //        obstacle.GetComponent<Rigidbody>().MovePosition(dir -  moveStep);
    //        if (moveStep.x > 0)
    //        {
    //            moveStep.x+= step;
    //        }
    //        else if (moveStep.x < 0)
    //        {
    //            moveStep.x-=step;
    //        }
    //        if (moveStep.z > 0)
    //        {
    //            moveStep.z+= step;
    //        }
    //        else if (moveStep.z < 0)
    //        {
    //            moveStep.z-= step;
    //        }

    //        yield return null;// new WaitForSeconds(0.1f);
    //    }
    //}
}
