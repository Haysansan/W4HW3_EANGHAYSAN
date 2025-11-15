using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallTorque : MonoBehaviour
{
    public float torqueAmount = 5f;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 torque = new Vector3(v, 0f, -h) * torqueAmount;

        rb.AddTorque(torque, ForceMode.Force);
        rb.maxAngularVelocity = 20f;

    }
}
