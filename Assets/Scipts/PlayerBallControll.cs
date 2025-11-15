using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallControl : MonoBehaviour
{
    public float torqueAmount = 5f;        // strength of spin
    public float jumpForce = 5f;           // impulse for jump (optional)
    public float maxAngularVel = 20f;      // clamp spinning speed
    public GoalHazardHandler goalHandler;

    Rigidbody rb;
    bool canJump = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularVel; // prevent runaway spin
    }

    void FixedUpdate()
    {
        // read input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // torque vector: rotate around X/Z axes for forward/backward & sideways roll
        Vector3 torque = new Vector3(v, 0f, -h) * torqueAmount;

        // apply torque
        rb.AddTorque(torque, ForceMode.Force);
    }

    void Update()
    {
        // jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }

        // restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // reset jump when hitting ground
        if (collision.gameObject.CompareTag("Ground") || collision.contacts.Length > 0)
        {
            canJump = true;
        }

        // hazard
        if (collision.gameObject.CompareTag("Hazard"))
        {
            goalHandler?.OnHazardHit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            goalHandler?.OnGoalReached();
        }
    }
}
