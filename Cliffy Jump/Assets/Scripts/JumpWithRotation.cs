using UnityEngine;

public class JumpWithRotation : MonoBehaviour
{
    public float jumpForce = 10f;            // Force of the jump
    public float rotationSpeed = 360f;      // Speed of the flip rotation (degrees per second)
    public float flipDuration = 0.5f;       // Duration of the front flip in seconds

    private Rigidbody rb;
    private bool isJumping = false;    
    private bool isFlipping = false;       
    private float flipTimer = 0f;           // Timer to track flip duration

    private Vector3 previousPosition;
    private Vector3 horizontalMovement;     // horizontal direction of the cube
    private Vector3 rotationAxis;           // axis of cube rotation

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, 6 * -9.81f, 0);
        previousPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }

        // Handle rotation during the flip
        if (isFlipping)
        {
            Flip();
        }

        previousPosition = transform.position;
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        isJumping = true;
        isFlipping = true;
        flipTimer = 0f;
    }

    private void Flip()
    {
        horizontalMovement = new Vector3(transform.position.x - previousPosition.x, 0, transform.position.z - previousPosition.z).normalized;
        if (horizontalMovement.magnitude > 0.01f)
        {
            rotationAxis = Vector3.Cross(Vector3.up, horizontalMovement);
        }
        // Rotate the cube forward
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis * rotationAmount);

        flipTimer += Time.deltaTime;

        // Stop the flip after the specified duration
        if (flipTimer >= flipDuration)
        {
            isFlipping = false;
            isJumping = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset jump state when the cube lands on a surface
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping =false;
            transform.rotation = Quaternion.Euler(Mathf.Round(transform.eulerAngles.x / 360) * 360, 0, 0);
        }
    }
}
