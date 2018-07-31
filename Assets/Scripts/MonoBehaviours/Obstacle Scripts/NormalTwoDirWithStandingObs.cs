using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTwoDirWithStandingObs : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> normalObstacles;
    private GameObject instantiatedObstacle;
    [SerializeField]
    private float positionFromCenter, SpawnTime, destroyTime, organizedForce;
    private int objectToSpawn, randRes;
    [SerializeField]
    public bool normalTwoDirectionWithStandingMode;
    private Direction dir;


    void Start()
    {
        InvokeRepeating("SpawnObstacles", 0, SpawnTime);
    }

    private void SpawnObstacles()
    {
        if (normalTwoDirectionWithStandingMode)
        {
            objectToSpawn = Random.Range(0, normalObstacles.Count);
            dir = (Direction)Random.Range(0, 8);
            switch (dir)
            {
                case Direction.Up:
                    instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(0, 0, positionFromCenter), normalObstacles[objectToSpawn].transform.rotation);
                    instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -organizedForce), ForceMode.Force);
                    break;

                case Direction.Down:
                    instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(0, 0, -positionFromCenter), normalObstacles[objectToSpawn].transform.rotation);
                    instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, organizedForce), ForceMode.Force);
                    break;

                case Direction.Left:
                    instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(-positionFromCenter, 0, 0), normalObstacles[objectToSpawn].transform.rotation);
                    if (normalObstacles[objectToSpawn].tag == "TallObstacle")
                    {
                        instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                    }
                    instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(organizedForce, 0, 0), ForceMode.Force);
                    break;

                case Direction.Right:
                    instantiatedObstacle = Instantiate(normalObstacles[objectToSpawn], new Vector3(positionFromCenter, 0, 0), normalObstacles[objectToSpawn].transform.rotation);
                    if (normalObstacles[objectToSpawn].tag == "TallObstacle")
                    {
                        instantiatedObstacle.transform.Rotate(new Vector3(0, 90, 0));
                    }
                    instantiatedObstacle.GetComponent<Rigidbody>().AddForce(new Vector3(-organizedForce, 0, 0), ForceMode.Force);
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
