using UnityEngine;
using UnityEngine.Animations;

public class Playerscript : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] Transform cameraTransform;

    CharacterController controller;
    Vector3 velocity;
    Vector2 look;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
    }

    void UpdateGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -1f; // keeps player "stuck" to ground
        }
        else
        {
            velocity += Physics.gravity * Time.deltaTime;
        }
    }

    void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }


        // transform.Translate(input * movementSpeed * Time.deltaTime, Space.World);
        controller.Move((input * movementSpeed + velocity) * Time.deltaTime);
    }
    void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X");
        look.y += Input.GetAxis("Mouse Y");

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
    }
}
