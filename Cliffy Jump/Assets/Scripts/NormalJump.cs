using UnityEngine;

public class NormalJump : MonoBehaviour
{
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private bool jumpRequested = false; // Señal para el salto
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, 6 * -9.81f, 0);
    }

    void Update()
    {
        // Detecta la entrada del usuario
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        // Aplica la física del salto
        if (jumpRequested)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
            jumpRequested = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detecta si el personaje está en el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
