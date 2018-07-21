using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyMove, keyJump;

    [SerializeField]
    private float speed, jumpSpeed;

    [SerializeField]
    private Transform otherSide;

    [SerializeField]
    private float angle;

    private Rigidbody rb;

    private Vector3 dirForward;

    private bool isMoving, isGrounded;

    public Vector3 DirForward
    {
        get
        {
            return dirForward;
        }

        set
        {
            dirForward = value;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyMove))
        {
            isMoving = true;
        }

        if (Input.GetKeyUp(keyMove))
        {
            isMoving = false;
        }

        if (Input.GetKeyDown(keyJump) && isGrounded)
        {
            StartCoroutine(Jump());
        }
    }

    private void FixedUpdate()
    {
        DirForward = (otherSide.position - transform.position).normalized;
        DirForward = Quaternion.AngleAxis(angle, Vector3.up) * DirForward;

        if (isMoving && isGrounded)
        {
            rb.AddForce(DirForward * speed);
        }
    }

    private IEnumerator Jump()
    {
        yield return new WaitForFixedUpdate();
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        isGrounded = false;
    }

    public void SwitchState()
    {
        isMoving = !isMoving;
    }
}
