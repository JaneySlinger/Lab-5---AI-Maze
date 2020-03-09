using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float verticalVelocity = 0;
    public float movementSpeed = 5;
    public float jumpHeight = 5;
    public float cameraSpeedVertical = 5;
    public float cameraSpeedHorizontal = 10;
    float y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate the player object about the Y axis
        //look left and right
        float rotation = Input.GetAxis("Mouse X");
        rotation *= cameraSpeedHorizontal;
        transform.Rotate(0,rotation,0);


        //rotate camera up and down around the player x axis
        float upDown = Input.GetAxis("Mouse Y");
        //clamp allowed rotation to 30
        if(y + upDown > 50 || y + upDown < 0 ){
            upDown = 0;
        }
        y += upDown;
        Camera.main.transform.RotateAround(transform.position, transform.right, upDown);
        Camera.main.transform.LookAt(transform);



        //moving forwards and backwards
        float forwardSpeed = Input.GetAxis("Vertical");
        forwardSpeed *= movementSpeed;

        //moving left and right
        float lateralSpeed = Input.GetAxis("Horizontal");
        lateralSpeed *= movementSpeed;

        //apply gravity
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        CharacterController characterController = GetComponent<CharacterController>();

        if (Input.GetButton("Jump") && characterController.isGrounded){
            verticalVelocity = jumpHeight;
        }

        Vector3 speed = new Vector3(lateralSpeed, verticalVelocity, forwardSpeed);

        //transform speed relative to player rotation to make them move forward, not north
        speed = transform.rotation * speed;

        //move at different speed to make up for variable framerates
        characterController.Move(speed * Time.deltaTime);
    }
}
