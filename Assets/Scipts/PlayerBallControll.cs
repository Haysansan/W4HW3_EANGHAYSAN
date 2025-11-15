using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallControl : MonoBehaviour
{
    bool gameFrozen = false;
    public float torqueAmount = 5f;
    public float jumpForce = 5f;
    public float maxAngularVel = 20f;
    public GoalHazardHandler goalHandler;
    public AudioClip hitClip;
    public GameObject hitParticlePrefab;


    Rigidbody rb;
    bool canJump = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularVel;
    }

    void FixedUpdate()
    {
        if (gameFrozen) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 torque = new Vector3(v, 0f, -h) * torqueAmount;

        rb.AddTorque(torque, ForceMode.Force);
    }

    void Update()
    {
        if (!gameFrozen && Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }

        if (collision.gameObject.CompareTag("Hazard"))
        {
            // freeze physics
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;

            gameFrozen = true;

            if (hitClip != null)
                AudioSource.PlayClipAtPoint(hitClip, collision.contacts[0].point);

            if (hitParticlePrefab != null)
            {
                ContactPoint contact = collision.contacts[0];
                Instantiate(hitParticlePrefab, contact.point, Quaternion.LookRotation(contact.normal));
            }

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
