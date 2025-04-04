using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float friction = 0.2f;
    [SerializeField] float brakeStrength = 2f;
    [SerializeField] float maxSpeed = 15f;
    [SerializeField] float maxBackwardSpeed = -5f;
    [SerializeField] float maxRotationSpeed = 50f;
    [SerializeField] float maxHandbrakingRotationSpeed = 100f;
    [SerializeField] float handBrakeStrenght = 10f;
    [SerializeField] float handBrakeRotationSpeed = 200f;
    [SerializeField] Rigidbody rb;
    [SerializeField] TrailRenderer handBrakeMarks;

    private bool accelerating;
    private bool decelerating;
    private float rotationInput;
    private bool handBraking;

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
        HandBrake();
    }

    void MovementInput()
    {
        rotationInput = 0;

        accelerating = Input.GetKey(KeyCode.W);

        decelerating = Input.GetKey(KeyCode.S);

        handBraking = Input.GetKey(KeyCode.Space);

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
                rb.velocity /= 1 + (brakeStrength * Time.fixedDeltaTime);
            }
        }

        if (rb.angularVelocity.magnitude < (handBraking ? maxRotationSpeed : maxHandbrakingRotationSpeed))
        {
            rb.AddTorque(rotationInput * rotationSpeed * transform.up * Time.fixedDeltaTime);
        }
    }

    void HandBrake()
    {
        if (handBraking)
        {
            rb.velocity /= 1 + (handBrakeStrenght * Time.fixedDeltaTime);
            rb.AddTorque(handBrakeRotationSpeed * rotationInput * transform.up * Time.fixedDeltaTime);
            handBrakeMarks.emitting = true;
        }
        else handBrakeMarks.emitting = false;
    }
}
