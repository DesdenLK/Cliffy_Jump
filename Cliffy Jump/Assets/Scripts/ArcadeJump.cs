using UnityEngine;

public class ArcadeJump : MonoBehaviour
{
    public float jumpForce;        // Velocidad inicial del salto
    //public float fallMultiplier = 3f;   // Velocidad de caida aumentada
    public float maxJumpHeight;    // Altura maxima que puede alcanzar el salto
    public float moveSpeed;

    public float frontFlipStartHeight;
    public float frontFlipTime;      // en segundos
    private float flipCurrentTime = 0f;     // en segundos

    private bool isGrounded = true;     // Si el objeto esta en el suelo
    private bool isJumping = false;     // Si esta en pleno salto
    private bool isFrontFlipping = false;
    private bool flipAvailable = true;      // true si esta jumping y no ha empezado el flip o si esta en el suelo

    private Rigidbody rb;               // Referencia al Rigidbody
    private float initialY;             // Posicion Y donde empeza el salto

    private Animator playerAnimator;
    private Vector3 flipAxis;       // eje de rotacion para el front flip


    private float angleRotations;

    public bool IsJumping
    {
        get { return isJumping; }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -9.81f, 0);
        flipAxis = new Vector3(1f, 0f, 0f);
    }


    void Jump()
    {
        isGrounded = false;             // Ya no esta en el suelo
        isJumping = true;               // Empieza a saltar
        initialY = transform.position.y; // Guarda la posicion inicial
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); // Asigna velocidad inicial
        Vector3 forwardSpeed = transform.forward.normalized * moveSpeed;
        rb.linearVelocity = new Vector3(forwardSpeed.x, 0, forwardSpeed.z);
        rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
    }


    void FrontFlipAction() 
    {
        if (flipCurrentTime < frontFlipTime && angleRotations < 360f)
        {
            float angle = (360f / frontFlipTime) * Time.deltaTime;
            angleRotations += angle;
            transform.Rotate(flipAxis, angle); 
            flipCurrentTime += Time.deltaTime;
        }
        else
        {
            flipCurrentTime = 0f;
            isFrontFlipping = false;
            flipAvailable = false;
        }
    }


    void Update()
    {
        if (isFrontFlipping) FrontFlipAction();
        // Detecta salto solo si esta en el suelo
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Jump();
        }

        // Empieza front flip
        if (isJumping && !isFrontFlipping && flipAvailable && transform.position.y >= initialY + frontFlipStartHeight)
        {
            angleRotations = 0;
            isFrontFlipping = true;
            flipCurrentTime = 0f;
            FrontFlipAction();
        }

    }


    void OnCollisionEnter(Collision collision)
    {
        // Detecta si el objeto toca el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            isFrontFlipping = false;
            flipAvailable = true;
        }
    }
}
