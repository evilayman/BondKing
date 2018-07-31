using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// on upV,downV,rightV,leftV use only tall obstacles
public class NormalOneDirWithStandingObs : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> normalObstacles;
    private GameObject instantiatedObstacle;
    [SerializeField]
    private float positionFromCenter, SpawnTime, destroyTime, organizedForce;
    private int objectToSpawn, randRes, upObsCnt, downObsCnt, leftObsCnt, rightObsCnt;
    [SerializeField]
    public bool normalOneDirectionWithStandingMode;
    private Direction dir;
    private bool canSpawnUp, canSpawnDown, canSpawnLeft, canSpawnRight, randomFailed;
    private float upDownTime, leftRightTime;

    void Start()
    {
        randomFailed = true;
        canSpawnUp = canSpawnDown = canSpawnLeft = canSpawnRight = true;
        upObsCnt = downObsCnt = leftObsCnt = rightObsCnt = 0;
        InvokeRepeating("SpawnObstacles", 0, SpawnTime);
    }

    private void SpawnObstacles()
    {
        if (normalOneDirectionWithStandingMode)
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
                dir = (Direction)Random.Range(0, 8);
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
                    case Direction.UpV:
                        randomFailed = false;
                        break;
                    case Direction.DownV:
                        randomFailed = false;
                        break;
                    case Direction.LeftV:
                        randomFailed = false;
                        break;
                    case Direction.RightV:
                        randomFailed = false;
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
                    }

                    break;

                case Direction.UpV:
                    instantiatedObstacle = Instantiate(normalObstacles[0], new Vector3(Random.Range(-positionFromCenter, positionFromCenter), 0, 80), Quaternion.identity);
                    instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                    instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -organizedForce), ForceMode.Force);
                    break;

                case Direction.DownV:
                    instantiatedObstacle = Instantiate(normalObstacles[0], new Vector3(Random.Range(-positionFromCenter, positionFromCenter), 0, -80), Quaternion.identity);
                    instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                    instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, organizedForce), ForceMode.Force);
                    break;

                case Direction.LeftV:
                    instantiatedObstacle = Instantiate(normalObstacles[0], new Vector3(-80, 0, Random.Range(-positionFromCenter, positionFromCenter)), Quaternion.identity);
                    instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(organizedForce, 0, 0), ForceMode.Force);
                    break;

                case Direction.RightV:
                    instantiatedObstacle = Instantiate(normalObstacles[0], new Vector3(80, 0, Random.Range(-positionFromCenter, positionFromCenter)), Quaternion.identity);
                    instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(-organizedForce, 0, 0), ForceMode.Force);
                    break;

                default:
                    break;


            }
            if (instantiatedObstacle.tag == "StandingObstacle")
            {
                Destroy(instantiatedObstacle, destroyTime / 2);

            }
            else
            {
                Destroy(instantiatedObstacle, destroyTime);


            }

        }
    }



}
