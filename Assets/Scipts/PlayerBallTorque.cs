using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallTorque : MonoBehaviour
{
    public float torqueAmount = 5f;  // Strength of spin
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input
        float h = Input.GetAxis("Horizontal");  // A/D or Left/Right
        float v = Input.GetAxis("Vertical");    // W/S or Up/Down

        // Calculate torque vector
        Vector3 torque = new Vector3(v, 0f, -h) * torqueAmount;

        // Apply torque to Rigidbody
        rb.AddTorque(torque, ForceMode.Force);
        rb.maxAngularVelocity = 20f;

    }
}
