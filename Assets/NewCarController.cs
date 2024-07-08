using UnityEngine;
using TMPro;

public class NewCarController : MonoBehaviour
{
    public Rigidbody sphereRB; // declare rigidbody for car

  //  public TextMeshProUGUI speed; // UI textmeshpro for the speed of the car

    public float fwdSpeed; // declare float for the fwdspeed of the car
    public float revSpeed; // declare float for the reverse speed of the car
    public float turnSpeed; // declare float to hold the value for the turn speed
    public LayerMask groundLayer; // declare layermask for the ground

    private float moveInput; // declare for the controls of the car - attached to input.getaxisraw vertical - forward and back
    private float turnInput; // declare float for the turning of the car - attached to the input.getaxisraw horizontal - left and right
    private bool isCarGrounded; // declare bool the check if the car is grounded

  //  public GameObject solidWall;
  //  public GameObject breakableWall;

    public float turnAcceleration; // declare float to hold the value of how sharp the car can turn
    public float acceleration;// = 0.01f; // declare float for accelleration of the car
    public float maxSpeed;// = 200; // declare float to hold the value of the max speed of the car
    public float minSpeed; // declare float to hold the value of the minimum speed the car can go before the speed is zerod out and the car stops
    public float maxTurn; // declare float for the max turn of the car, to stop it over turning left and right
    public float minTurn; // declare float for the minimum turn of the car. to stop it going into negative numbers and messing up the turning of the car
    public Vector3 velocity;
   
    // private float normalDrag;
    //public float modifiedDrag;

    public float airDrag; // declare float for the drag the car recieves in the air 
    public float groundDrag; // declare float for the drag of the car when on the ground

    public float alignToGroundTime; // declare float to set time it takes for the car to align to the ground

    void Start()
    {
        // Detach Sphere from car
        sphereRB.transform.parent = null;

        //normalDrag = sphereRB.drag;
    }

    void Update()
    {
        // Get Input
        moveInput = Input.GetAxisRaw("Vertical"); //  set move input float value to the forward and backwards movement of the car - set to W,S Uparrow and Downarrow respectivly
        turnInput = Input.GetAxisRaw("Horizontal"); // set turn input for left and right movement of the car - set to W,A left arrow and right arrow respectivly

        //speed.text = fwdSpeed.ToString("0"); //  link fwdspeed of car to UI speed text and set it to integers only
        if (Input.GetKey(KeyCode.W)) // if W is pressed
        {
            fwdSpeed += acceleration; // move forward / increase forward speed by increments set by the acceleration value
            turnSpeed += turnAcceleration; // increase turn speed by increments set by the turnacceleration value
        }
        if (Input.GetKey(KeyCode.S)) // if S is pressed and held
        {
            fwdSpeed -= acceleration; // move backwards by decreasing forward speed by increments set by the acceleration value
            turnSpeed += turnAcceleration; // reduce turn speeed by increments set by the turnacceleration value
        }
/*
        if(fwdSpeed > 49)
        {
            solidWall.gameObject.SetActive(false);
            breakableWall.gameObject.SetActive(true);
        }

        if(fwdSpeed < 49)
        {
            solidWall.gameObject.SetActive(true);
            breakableWall.gameObject.SetActive(false);
        }
*/
        if (moveInput == 0) // if moveinput is 0 --- MAY BE REDUNDNANT SOON
        {
            fwdSpeed -= acceleration; // reduce forward speed by value set by accelleration value
            turnSpeed -= turnAcceleration; // reduce turn speed by value set by turnacceleration value
        }

        velocity = sphereRB.velocity; // set velocity valye to the cars rigidbody velocity speed

        if ( fwdSpeed > maxSpeed) // if forward speed is greated than the max speed
        {
            fwdSpeed = maxSpeed; // keep car at full speed until slowing down or stopping
        }

        if (fwdSpeed < minSpeed) // if forward speed is less than minimum speed
        {
            fwdSpeed = minSpeed;// == forward speed is kept at 0
        }

        if(turnSpeed > maxTurn) // if turn speed is greater than the max turn
        {
            turnSpeed = maxTurn; // turn speed at max
        }

        if (turnSpeed < minTurn) // if turn speed is less than the minimum turn
        {
            turnSpeed = minTurn; // keep turn at min
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