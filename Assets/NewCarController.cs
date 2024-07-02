using UnityEngine;
using TMPro;

public class NewCarController : MonoBehaviour
{
    public Rigidbody sphereRB;

    public TextMeshProUGUI speed;

    public float fwdSpeed;
    public float revSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;

    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;

    public float turnAcceleration;
    public float acceleration;// = 0.01f;
    public float maxSpeed;// = 200;
    public float minSpeed;
    public float maxTurn;
    public float minTurn;
    public Vector3 velocity;
   

   // private float normalDrag;
    //public float modifiedDrag;

    public float airDrag;
    public float groundDrag;

    public float alignToGroundTime;

    void Start()
    {
        // Detach Sphere from car
        sphereRB.transform.parent = null;

        //normalDrag = sphereRB.drag;
    }

    void Update()
    {
        // Get Input
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        speed.text = fwdSpeed.ToString("0");
        if (Input.GetKey(KeyCode.W))
        {
            fwdSpeed += acceleration;
            turnSpeed += turnAcceleration;
        }
        if (Input.GetKey(KeyCode.S))
        {
            fwdSpeed -= acceleration;
            turnSpeed += turnAcceleration;
        }

        //if(sphereRB.velocity.x <= 2f)
       // {
       //     fwdSpeed = 0;
       // }

        if (moveInput == 0)
        {
            fwdSpeed -= acceleration;
            turnSpeed -= turnAcceleration;
        }

        velocity = sphereRB.velocity;

        if ( fwdSpeed > maxSpeed)
        {
            fwdSpeed = maxSpeed;
        }

        if (fwdSpeed < minSpeed)
        {
            fwdSpeed = minSpeed;
        }

        if(turnSpeed > maxTurn)
        {
            turnSpeed = maxTurn;
        }

        if (turnSpeed < minTurn)
        {
            turnSpeed = minTurn;
        }
        // Calculate Turning Rotation
        float newRot = turnInput * turnSpeed * Time.deltaTime * moveInput;

        //if (isCarGrounded)

        
        // Set Cars Position to Our Sphere
        transform.position = sphereRB.transform.position;

        // Raycast to the ground and get normal to align car with it.
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        // Rotate Car to align with ground
        /* Quaternion toRotateTo*/
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        // transform.rotation = Quaternion.Slerp(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);
        //transform.rotation = Quaternion.FromToRotation(transform.rotation, toRotateTo, alignToGroundTime * Time.deltaTime);
        // Calculate Movement Direction
        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed;
        if (isCarGrounded)
        {
            transform.Rotate(0, newRot, 0, Space.World);
            sphereRB.drag = groundDrag;
        }
        else
        {
            sphereRB.drag = airDrag;
        }
                

        // Calculate Drag
        //sphereRB.drag = isCarGrounded ? normalDrag : modifiedDrag;
    }

    private void FixedUpdate()
    {
        if (isCarGrounded)
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration); // Add Movement
        else
            sphereRB.AddForce(transform.up * -30f); // Add Gravity
    }
}