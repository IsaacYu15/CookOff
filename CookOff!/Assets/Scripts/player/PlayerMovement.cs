using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber = 1;              
    public float speed = 12f;
    public float turnSpeed = 0.5f;

    private Vector3 movementInputValue;

    private string vertAxis;
    private string horzAxis;
    private Rigidbody rigidBody;
    private ConfigurableJoint joint;
    public Animator animator;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Start()
    {
        //assign keys based on player number
        vertAxis = "Vertical" + playerNumber;
        horzAxis = "Horizontal" + playerNumber;

        movementInputValue = Vector3.zero;

    }

    private void Update() //update is better suited to handle input
    {
        movementInputValue = new Vector3(Input.GetAxis(horzAxis), 0, Input.GetAxis(vertAxis)).normalized; //make suring moving is always a magnitude of 1

        if (movementInputValue != Vector3.zero)
        {

            float targetAngle = Mathf.Atan2(movementInputValue.x, movementInputValue.z) * Mathf.Rad2Deg; //calculate turn radius in radians, convert into degrees
            joint.targetRotation = Quaternion.Euler(targetAngle, 0, 0);
            animator.SetBool("walk", true); //trigger walking anim
        } else
        {
            movementInputValue = Vector3.zero;
            animator.SetBool("walk", false); //idle anim
        }


    }

    private void FixedUpdate() //use fixedupdate to handle physics
    {
        rigidBody.velocity = movementInputValue * speed * Time.deltaTime;
    }





}
