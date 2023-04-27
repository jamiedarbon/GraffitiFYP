using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private bool grounded;
    public float speed = 100.0f;
    public float maxSpeed = 10.0f;
    public float jumpHeight = 10.0f;
    public KeyCode jumpKey = KeyCode.Space;
    public Transform camera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = camera.forward * moveVertical + camera.right * moveHorizontal;

        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (grounded && Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            moveDirection = new Vector3(rb.velocity.x / 2, rb.velocity.y / 2, rb.velocity.z / 2);
            rb.velocity = moveDirection;
        }
        else
        {
            if (rb.velocity.magnitude > maxSpeed)
            {
                Debug.Log(rb.velocity.magnitude);
                return;
            }
            if (rb.velocity.x > maxSpeed)
            {
                rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
                moveDirection = rb.velocity;
                return;
            }
            if (rb.velocity.z > maxSpeed)
            {
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed);
                moveDirection = rb.velocity;
                return;
            }
            rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);
        }
    }

    private void Jump()
    {
        if (grounded)
        {
            rb.AddForce(0f, jumpHeight, 0f, ForceMode.Impulse);
            grounded = false;
        }
    }
}
