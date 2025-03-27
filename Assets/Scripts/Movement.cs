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
        if (accelerating)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.velocity += speed * transform.right * Time.fixedDeltaTime;
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
            float forwardVelocity = Vector3.Dot(rb.velocity, transform.right);

            if (forwardVelocity > 0) 
            {
                rb.velocity -= brakeStrength * transform.right * Time.fixedDeltaTime;

            }
            else if (forwardVelocity <= 0) 
            {
                if (rb.velocity.magnitude < maxSpeed)
                {
                    rb.velocity -= speed * transform.right * Time.fixedDeltaTime;
                }
            }
        }

        if (rb.angularVelocity.magnitude < maxRotationSpeed)
        {
            rb.AddTorque(rotationInput * rotationSpeed * transform.up * Time.fixedDeltaTime);
        }
    }

}
