using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class NormalJump : MonoBehaviour
{
    public float jumpForce = 10f;
    private bool isGrounded = true;
    public bool jumpRequested = false; // Se�al para el salto
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, 6 * -9.81f, 0);
    }

    void Update()
    {

        // Detecta la entrada del usuario
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown((int)MouseButton.Left)) && isGrounded)
        {
            jumpRequested = true;
        }
    }

    public void Jump()
    {
        jumpRequested = true;
    }

    void FixedUpdate()
    {
        // Aplica la f�sica del salto
        if (jumpRequested)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            isGrounded = false;
            jumpRequested = false;
        }

        CheckGrounded(); // Comprueba si el jugador est� en el suelo
    }

    void CheckGrounded()
    {
        float groundCheckDistance = 0.55f; // Distancia para detectar el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
