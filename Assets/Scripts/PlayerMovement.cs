using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 200;

    [SerializeField]
    private float strafeSpeed = 200;

    [SerializeField]
    private float rotateSpeed = 100;

    [SerializeField]
    private float jumpForce = 10;

    [SerializeField]
    private float glideFallSpeed = 1;

    private Rigidbody rb;

    [HideInInspector]
    public bool grounded = false;

    private bool jumped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Rotate();
        Jump();
    }

    private void Jump()
    {
        if (Input.GetAxis("Jump") != 0 && grounded && !jumped)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumped = true;
        }
        else if (Input.GetAxis("Jump") != 0 && !grounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, -glideFallSpeed, rb.velocity.z);
        }
        else if (Input.GetAxis("Jump") == 0 && grounded)
        {
            jumped = false;
        }
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * Time.deltaTime * rotateSpeed, Space.World);
    }

    private void Move()
    {
        Vector3 sVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
        {
            sVelocity = -transform.right * strafeSpeed * Time.deltaTime;
            rb.velocity = new Vector3(sVelocity.x, rb.velocity.y, sVelocity.z);
        }

        if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            sVelocity = transform.right * strafeSpeed * Time.deltaTime;
            rb.velocity = new Vector3(sVelocity.x, rb.velocity.y, sVelocity.z);
        }

        Vector3 velocity = transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        velocity += sVelocity;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
}
