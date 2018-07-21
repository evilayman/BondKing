using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float arrowDistance;

    private MoveForward mFTarget;

    private void Start()
    {
        mFTarget = target.GetComponent<MoveForward>();
    }

    private void Update()
    {
        transform.position = target.position + (mFTarget.DirForward * arrowDistance);

        if (mFTarget.DirForward != Vector3.zero)
            transform.forward = mFTarget.DirForward;
    }
}
