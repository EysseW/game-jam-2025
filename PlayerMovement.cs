using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
    public float wobbleAmount = 0.01f;
    public float wobbleSpeed = 1.0f;

    public Transform groundCheck;
    public Camera playerCamera;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float wobbleIndex = 0f;
    private bool risingInWobble;

    Vector3 velocity;
    private Vector3 currentPos;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Resetting the default velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;

        if (isMoving)
        {
            currentPos = playerCamera.transform.position;
            currentPos.y += wobbleIndex * wobbleAmount;
            print(currentPos.y);
            playerCamera.transform.position = currentPos;

            if (risingInWobble)
            {
                wobbleIndex += 0.1f * wobbleSpeed;
            }
            else
            {
                wobbleIndex -= 0.1f * wobbleSpeed;
            }
            if (wobbleIndex >= 20f)
            {
                risingInWobble = false;
            }
            else if (wobbleIndex <= -20f)
            {
                risingInWobble = true;
            }
        }
    }
}
