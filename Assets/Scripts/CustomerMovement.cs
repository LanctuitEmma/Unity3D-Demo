using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    public float speed = 4.0f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    Vector3 velocity;
    bool isGrounded;
    public Transform groundCheck;// Check where is the ground relative to the player.
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private CharacterController charCont;

    // Use this for initialization
    void Start()
    {
        charCont = GetComponent<CharacterController>();

        // if there is a position that should be restored
        if (DataScript.PlayerPosition != Vector3.zero)
        {
            gameObject.transform.position = DataScript.PlayerPosition;
            gameObject.transform.rotation = DataScript.PlayerRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;//Let's put some gravity in our script.
        charCont.Move(velocity * Time.deltaTime);

        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed); //Limits the max speed of the player

        // movement.y = gravity;

        movement *= Time.deltaTime;     //Ensures the speed the player moves does not change based on frame rate
        movement = transform.TransformDirection(movement);
        charCont.Move(movement);
    }
}
