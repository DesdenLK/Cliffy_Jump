using UnityEngine;

public class ArcadeJump : MonoBehaviour
{
    public float jumpForce;        // Velocidad inicial del salto
    public float fallMultiplier;   // Velocidad de caida aumentada

    public float frontFlipStartHeight;
    public float frontFlipTime;      // en segundos
    private float flipCurrentTime = 0f;     // en segundos

    private bool isGrounded = true;     // Si el objeto esta en el suelo
    private bool isJumping = false;     // Si esta en pleno salto
    private bool isFrontFlipping = false;   // true si esta haciendo frontflip
    private bool flipAvailable = true;      // true si esta jumping y no ha empezado el flip o si esta en el suelo

    private bool jumpRequested = false;

    private Rigidbody rb;               // Referencia al Rigidbody
    private float initialY;             // Posicion Y donde empeza el salto

    private Animator playerAnimator;
    private Vector3 flipAxis;       // eje de rotacion para el front flip


    private float angleRotations;
    
    private AutoJump autoJump;

    public bool IsFrontFlipping
    {
        get { return isFrontFlipping; }
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, 6*-9.81f, 0);
        flipAxis = new Vector3(1f, 0f, 0f);
        autoJump = GetComponent<AutoJump>();
    }


    public void Jump()
    {
        jumpRequested = true;
        isJumping = false;
        isFrontFlipping = false;
        flipAvailable = true;
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
        // Inicio salto
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        if (jumpRequested)
        {
            isGrounded = false;             // Ya no esta en el suelo
            isJumping = true;               // Empieza a saltar
            initialY = transform.position.y; // Guarda la posicion inicial
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumpRequested = false;
        }


        
        // Control de front flip
        if (isFrontFlipping) FrontFlipAction();

        // Empieza front flip
        if (isJumping && !isFrontFlipping && flipAvailable)
        {
            Debug.Log("Frontflip");
            angleRotations = 0;
            isFrontFlipping = true;
            flipCurrentTime = 0f;
            FrontFlipAction();
        }
        if (!isFrontFlipping) UpdateGrounded();
    }


    void UpdateGrounded()
    {
        float groundCheckDistance = 0.55f; // Distancia para detectar el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (isGrounded)
        {
            isJumping = false;
            isFrontFlipping = false;
            flipAvailable = true;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        // Detecta si el objeto toca el suelo
        if (collision.gameObject.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            isJumping = false;
            isFrontFlipping = false;
            flipAvailable = true;
            Debug.Log("Ground");
        }
    }
}