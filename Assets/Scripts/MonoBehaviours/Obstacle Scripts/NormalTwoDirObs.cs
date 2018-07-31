using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTwoDirObs : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> normalObstacles;
    private GameObject instantiatedObstacle;
    [SerializeField]
    private float positionFromCenter, SpawnTime, destroyTime, organizedForce;
    private int objectToSpawn, randRes;
    [SerializeField]
    public bool normalTwoDirectionMode;
    private Direction dir;
    private float upDownTime, leftRightTime;

    void Start ()
    {
        InvokeRepeating("SpawnObstacles", 0, SpawnTime);
    }
    
    void SpawnObstacles()
    {
        if (normalTwoDirectionMode)
        {
            objectToSpawn = Random.Range(0, normalObstacles.Count);
            dir = (Direction)Random.Range(0, 4);
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
                default:
                    break;


            }
            Destroy(instantiatedObstacle, destroyTime);

        }
    }



}
