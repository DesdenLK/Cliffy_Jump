using UnityEngine;

public class ArcadeJump : MonoBehaviour
{
    public float jumpForce = 15f;        // Velocidad inicial del salto
    public float fallMultiplier = 3f;   // Velocidad de ca�da aumentada
    public float maxJumpHeight = 3f;    // Altura m�xima que puede alcanzar el salto

    private bool isGrounded = true;     // Si el objeto est� en el suelo
    private bool isJumping = false;     // Si est� en pleno salto
    private Rigidbody rb;               // Referencia al Rigidbody
    private float initialY;             // Posici�n Y donde empez� el salto

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Detecta salto solo si est� en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        isGrounded = false;             // Ya no est� en el suelo
        isJumping = true;               // Empieza a saltar
        initialY = transform.position.y; // Guarda la posici�n inicial
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); // Asigna velocidad inicial
    }

    void FixedUpdate()
    {
        // Controlar la subida para limitar la altura
        if (isJumping && transform.position.y >= initialY + maxJumpHeight)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Detiene la subida
            isJumping = false; // Marca que ya alcanz� la altura m�xima
        }

        // Acelerar la ca�da cuando el objeto est� descendiendo
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detecta si el objeto toca el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
}
