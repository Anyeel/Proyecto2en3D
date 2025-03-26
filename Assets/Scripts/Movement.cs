using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;        
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] float friction = 0.2f;
    [SerializeField] float brakeStrength = 2f;
    [SerializeField] float maxSpeed = 15f;
    [SerializeField] float maxRotationSpeed = 50f;
    [SerializeField] Rigidbody rb;

    private bool accelerating;
    private bool deccelerating;
    private float rotationInput;

    void Start()
    {

    }

    void Update()
    {
        MovementInput();
    }

    private void FixedUpdate()
    {
        MovementPhysics();
    }

    void MovementInput()
    {
        rotationInput = 0;

        accelerating = Input.GetKey(KeyCode.W);

        deccelerating = Input.GetKey(KeyCode.S);

        if (Input.GetKey(KeyCode.A))
        {
            rotationInput--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotationInput++;
        }
    }

    void MovementPhysics()
    {
        if (accelerating)
        {
            rb.velocity += speed * transform.right * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = new Vector3(
                rb.velocity.x * (1 / (1 + friction * Time.fixedDeltaTime)), 
                0, 
                rb.velocity.z * (1 / (1 + friction * Time.fixedDeltaTime))
            );
        }
        if (deccelerating)
        {
            // si estoy pulsando S -> dos cosas (el coche va hacia delante = frena, si va para atras lo acelera hacia atras)
        }

        rb.AddTorque(rotationInput * rotationSpeed * transform.up * Time.fixedDeltaTime);
    }

    void Brake()
    {
        rb.velocity = new Vector3(rb.velocity.x * (1 / (1 + brakeStrength * Time.fixedDeltaTime)), 0, rb.velocity.z * (1 / (1 + brakeStrength * Time.fixedDeltaTime)));
    }
}
