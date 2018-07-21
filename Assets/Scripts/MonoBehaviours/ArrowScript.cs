using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Raskulls.Variables;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private Vector3Variable targetPosition;

    [SerializeField]
    private FloatReference arrowDistance;

    [SerializeField]
    private Vector3Variable dirForward;

    private void FixedUpdate()
    {
        transform.position = targetPosition.Value + (dirForward.Value * arrowDistance.Value);

        if (dirForward.Value != Vector3.zero)
            transform.forward = dirForward.Value;
    }
}
