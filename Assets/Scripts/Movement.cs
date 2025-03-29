using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 200f;
    [SerializeField] float friction = 0.2f;
    [SerializeField] float brakeStrength = 2f;
    [SerializeField] float maxSpeed = 15f;
    [SerializeField] float maxBackwardSpeed = -5f;
    [SerializeField] float maxRotationSpeed = 50f;
    [SerializeField] Rigidbody rb;

    private bool accelerating;
    private bool decelerating;
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

        decelerating = Input.GetKey(KeyCode.S);

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
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);

        if (accelerating)
        {
            if (localVelocity.z < maxSpeed)
            {
                rb.velocity += speed * transform.forward * Time.fixedDeltaTime;
            }
        }
        else
        {
            rb.velocity = new Vector3(
                rb.velocity.x * Mathf.Clamp01(1 - friction * Time.fixedDeltaTime),
                0,
                rb.velocity.z * Mathf.Clamp01(1 - friction * Time.fixedDeltaTime)
            );
        }

        if (decelerating)
        {
            if (localVelocity.z < 0)
            {
                if (localVelocity.z > maxBackwardSpeed)
                {
                    rb.velocity -= speed * transform.forward * Time.fixedDeltaTime;
                }
            }
            else
            {
                rb.velocity -= brakeStrength * transform.forward * Time.fixedDeltaTime;
            }
        }

        if (rb.angularVelocity.magnitude < maxRotationSpeed)
        {
            rb.AddTorque(rotationInput * rotationSpeed * transform.up * Time.fixedDeltaTime);
        }
    }
}
