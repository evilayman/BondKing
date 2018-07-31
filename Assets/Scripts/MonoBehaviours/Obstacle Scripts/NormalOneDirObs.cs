using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalOneDirObs : MonoBehaviour
{

    [SerializeField]
    private List<GameObject>  normalObstacles;
    private GameObject instantiatedObstacle;
    [SerializeField]
    private float positionFromCenter, SpawnTime, destroyTime, organizedForce;
    private int objectToSpawn, randRes, upObsCnt, downObsCnt, leftObsCnt, rightObsCnt;
    [SerializeField]
    public bool normalOneDirectionMode;
    private Direction dir;
    private bool canSpawnUp, canSpawnDown, canSpawnLeft, canSpawnRight, randomFailed;
    private float upDownTime, leftRightTime;


    void Start ()
    {
        randomFailed = true;
        canSpawnUp = canSpawnDown = canSpawnLeft = canSpawnRight = true;
        upObsCnt = downObsCnt = leftObsCnt = rightObsCnt = 0;
        InvokeRepeating("SpawnObstacles", 0, SpawnTime);
    }
	
	private void SpawnObstacles()
    {
        if (normalOneDirectionMode)
        {
            if (!canSpawnUp)
            {
                if (Time.time - upDownTime >= destroyTime)
                {
                    canSpawnUp = true;
                    downObsCnt = 0;
                }
            }
            if (!canSpawnDown)
            {
                if (Time.time - upDownTime >= destroyTime)
                {
                    canSpawnDown = true;
                    upObsCnt = 0;
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
                            randomFailed = false;
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

                    if (upObsCnt < 3 && canSpawnUp)
                    {
                        canSpawnDown = false;
                        upObsCnt++;
                        instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(0, 0, positionFromCenter), normalObstacles[objectToSpawn].transform.rotation);
                        instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -organizedForce), ForceMode.Force);
                        upDownTime = Time.time;
                        Destroy(instantiatedObstacle, destroyTime);
                    }


                    break;

                case Direction.Down:
                    if (downObsCnt < 3 && canSpawnDown)
                    {
                        downObsCnt++;
                        instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(0, 0, -positionFromCenter), normalObstacles[objectToSpawn].transform.rotation);
                        instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, organizedForce), ForceMode.Force);
                        canSpawnUp = false;
                        upDownTime = Time.time;
                        Destroy(instantiatedObstacle, destroyTime);
                    }

                    break;

                case Direction.Left:
                    if (leftObsCnt < 3 && canSpawnLeft)
                    {
                        leftObsCnt++;
                        instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(-positionFromCenter, 0, 0), normalObstacles[objectToSpawn].transform.rotation);
                        if (normalObstacles[objectToSpawn].tag == "TallObstacle")
                        {
                            instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                        }

                        instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(organizedForce, 0, 0), ForceMode.Force);
                        canSpawnRight = false;
                        leftRightTime = Time.time;
                        Destroy(instantiatedObstacle, destroyTime);
                    }

                    break;

                case Direction.Right:
                    if (leftObsCnt < 3 && canSpawnRight)
                    {
                        rightObsCnt++;
                        instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(positionFromCenter, 0, 0), normalObstacles[objectToSpawn].transform.rotation);
                        if (normalObstacles[objectToSpawn].tag == "TallObstacle")
                        {
                            instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                        }
                        instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(-organizedForce, 0, 0), ForceMode.Force);
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
}
