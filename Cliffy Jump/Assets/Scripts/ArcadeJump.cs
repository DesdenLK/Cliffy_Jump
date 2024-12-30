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

    private Rigidbody rb;               // Referencia al Rigidbody
    private float initialY;             // Posicion Y donde empeza el salto

    private Animator playerAnimator;
    private Vector3 flipAxis;       // eje de rotacion para el front flip


    private float angleRotations;

    public bool IsFrontFlipping
    {
        get { return isFrontFlipping; }
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
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
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
            Jump();
        }

        // Control de velocidad lineal en eje y (vertical)
        float yVelocity = rb.linearVelocity.y;

        // En el aire una vez ha hecho el flip
        if (isJumping && !isFrontFlipping && !flipAvailable) 
        {
            yVelocity = Physics.gravity.y * fallMultiplier;
        }
        else if (isGrounded) yVelocity = 0;
        if (isJumping) rb.linearVelocity = new Vector3(0, yVelocity, 0);


        // Control de front flip
        if (isFrontFlipping) FrontFlipAction();

        // Empieza front flip
        if (isJumping && !isFrontFlipping && flipAvailable && transform.position.y >= initialY + frontFlipStartHeight)
        {
            angleRotations = 0;
            isFrontFlipping = true;
            flipCurrentTime = 0f;
            FrontFlipAction();
        }

    }


    /*void UpdateGrounded()
    {
        float groundCheckDistance = 0.05f; // Distancia para detectar el suelo
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }*/


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