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

    //[HideInInspector]
    public bool grounded = false;

    private bool jumped = false;
    private bool isStunned = false;
    private float stunReleaseTime;

    [SerializeField]
    private AudioClip jumpTweetA;
    [SerializeField]
    private AudioClip jumpTweetB;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isStunned)
        {
            if(Time.fixedTime > stunReleaseTime)
            {
                Unstun();
            }
        }
        else
        {
            Move();
            Jump();
        }
        Rotate();
    }

    private void Jump()
    {
        if (Input.GetAxis("Jump") != 0 && grounded && !jumped)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumped = true;
            playerAudio.PlayOneShot(jumpTweetA, 1.0f);
        }
        else if (Input.GetAxis("Jump") != 0 && !grounded && rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, -glideFallSpeed, rb.velocity.z);
            playerAudio.PlayOneShot(jumpTweetB, 0.1f);
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
        }
        if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            sVelocity = transform.right * strafeSpeed * Time.deltaTime;
        }

        Vector3 velocity = transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        velocity += sVelocity;

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }

    /// <summary>Locks the player's ability to move</summary>
    /// <param name="stunDuration">How long to stun the player for</param>
    public void Stun(float stunDuration)
    {
        isStunned = true;
        stunReleaseTime = Time.time + stunDuration;

        // Implement start of special animations/effects here.
    }
    private void Unstun()
    {
        isStunned = false;
        
        // Implement end of special animations/effects here.
    }
}
