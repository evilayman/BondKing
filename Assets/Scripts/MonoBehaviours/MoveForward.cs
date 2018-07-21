using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Raskulls.Variables;

public class MoveForward : MonoBehaviour
{
    [SerializeField]
    private KeyCode keyMove, keyJump;

    [SerializeField]
    private FloatReference speed, jumpSpeed;

    [SerializeField]
    private Vector3Variable myPosition, otherPosition, dirForward;

    [SerializeField]
    private FloatReference angle;

    private Rigidbody rb;

    private bool isMoving, isGrounded;

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
        myPosition.Value = transform.position;
        dirForward.Value = (otherPosition.Value - transform.position).normalized;
        dirForward.Value = Quaternion.AngleAxis(angle, Vector3.up) * dirForward.Value;

        if (isMoving && isGrounded)
        {
            rb.AddForce(dirForward.Value * speed.Value);
        }
    }

    private IEnumerator Jump()
    {
        yield return new WaitForFixedUpdate();
        rb.AddForce(Vector3.up * jumpSpeed.Value, ForceMode.Impulse);
        isGrounded = false;
    }

    public void SwitchState()
    {
        isMoving = !isMoving;
    }
}
