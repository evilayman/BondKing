using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private Transform side1Transform;
    [SerializeField]
    private Transform side2Transform;

    private Vector3 lookAtTarget = new Vector3();

    private void Update()
    {
        lookAtTarget.x = Mathf.Abs(side1Transform.position.x - side2Transform.position.x) / 2;
        lookAtTarget.y = Mathf.Abs(side1Transform.position.y - side2Transform.position.y) / 2;
        lookAtTarget.z = Mathf.Abs(side1Transform.position.z - side2Transform.position.z) / 2;

        transform.LookAt(lookAtTarget);

    }

}
