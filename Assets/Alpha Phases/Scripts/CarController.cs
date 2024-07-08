using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float moveInput; // declare variable to control fwd bck input
    private float turnInput; // declare variable to control left right input

    public float fwdSpeed;
    public float revSpeed;
    public float turnspeed;
    
        public Rigidbody sphereRB; // declare sphere Rigidbody

    // Start is called before the first frame update
    void Start()
    {
        sphereRB.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //get axis for controlling the car / sphere going forward backwards
        moveInput = Input.GetAxisRaw("Vertical");
        //get axis for controlling the car / sphere turning left and right
        turnInput = Input.GetAxisRaw("Horizontal");
        //if moveInput > 0, ? Checks if its true or not. If its true then we use forward speed, if false then we use reverse speed
        moveInput *= moveInput > 0 ? fwdSpeed : revSpeed; // times move input by fwdSpeed float

        float newRotation = turnInput * turnspeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        //set car rotation
        transform.Rotate(0, newRotation, 0,Space.World);

        //set car transform position to sphereRB transform position
        transform.position = sphereRB.transform.position;
    }


    private void FixedUpdate()
    {
        sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
    }
}
