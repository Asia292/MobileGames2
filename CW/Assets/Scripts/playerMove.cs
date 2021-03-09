using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float moveSpeed = 6.0f;      // Amplifies the force applied to player
    public Vector3 MoveVec;             // Vector to move player
    public Joystick joystick;       
    public GameObject pickUp;           // Button to pick up
    public GameObject upIcon;           // Button to go up
    public GameObject downIcon;         // Button to go down

    private Rigidbody thisRigidBody;    // Players rigid body
    private Animator anim;              
    private Transform cam;              // Camera
    private Vector3 camZ;               // Camera Z dir
    private Vector3 camX;               // Camera X dir

    GameObject[] items;                 // Items that can be picked up
    GameObject[] Up;                    // Gameobjects that are end pos for going up
    GameObject[] Down;                  // Gameobjects that are end pos for going down
    public GameObject obj;              // Obj being picked up

    //// ASSIGNS VARIABLES ////
    void Start()
    {
        thisRigidBody = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        cam = GameObject.Find("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //// GETS CAMERA VECTOR AND RESETS Y ROTATION - USED TO WORK OUT WHAT WAY PLAYER IS FACING ////
        camZ = cam.forward;
        camZ.y = 0;
        camZ.Normalize();
        
        camX = cam.right;
        camX.y = 0;
        camX.Normalize();


        MoveVec = PoolInput();      // Works out the vector to move player by from joystick

        //Debug.Log(MoveVec.z);
        //// ANIMATION CONTROLER - BASED OFF PLAYER MOVE VECTOR ////
        if (MoveVec.z > 0.4 && MoveVec.x > -0.3 && MoveVec.x < 0.3)
        {
            //Debug.Log("Run");
            anim.SetBool("Run", true);
            anim.SetBool("Idle", false);
            anim.SetBool("RunLeft", false);
            anim.SetBool("RunRight", false);
            anim.SetBool("Turn", false);
        }
        else if (MoveVec.x > 0 /*&& MoveVec.z < 0.6 && MoveVec.z > -0.7*/)
        {
            anim.SetBool("RunRight", true);
            anim.SetBool("Run", false);
            anim.SetBool("Idle", false);
            anim.SetBool("RunLeft", false);
            anim.SetBool("Turn", false);
            //Debug.Log("Right");
             /*Debug.Log(anim.GetBool("RunRight"));
             Debug.Log(anim.GetBool("Run"));*/
        }
        else if (MoveVec.x < 0 /*&& MoveVec.z < 0.6 && MoveVec.z > -0.7*/)
        {
            //Debug.Log("Left");
            anim.SetBool("RunLeft", true);
            anim.SetBool("Run", false);
            anim.SetBool("Idle", false);
            anim.SetBool("RunRight", false);
            anim.SetBool("Turn", false);
        }
        else if (MoveVec.z < -0.7 )
        {
            //Debug.Log("Turn");
            anim.SetBool("Turn", true);
            anim.SetBool("Run", false);
            anim.SetBool("Idle", false);
            anim.SetBool("RunRight", false);
            anim.SetBool("RunLeft", false);
        }
        else
        {
            //Debug.Log("Idle");
            anim.SetBool("Idle", true);
            anim.SetBool("Run", false);
            anim.SetBool("RunLeft", false);
            anim.SetBool("RunRight", false);
            anim.SetBool("Turn", false);
        }

        //// CHECKS TO SEE IF BUTTONS NEED TO BE ENABLED ////
        itemPickUp();
        ladderButtons();
        //Debug.Log("close to " + obj);
        

        Move(MoveVec);      // Moves player
    }

    //// APPLIES FORCE TO PLAYER RIGID BODY BASED ON VECTOR ////
    private void Move(Vector3 move)
    {
        //Debug.Log(move);
        thisRigidBody.AddForce((move.x * camX + move.z * camZ) * moveSpeed);
    }

    //// CALCULATES VECTOR TO MOVE PLAYER BY ////
    private Vector3 PoolInput()
    {
        Vector3 dir = Vector3.zero;

        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();

        if (dir.magnitude > 1)
            dir.Normalize();

        //Debug.Log(dir);

        return dir;
    }

    //// WHEN TO ENABLE/DISABLE ITEM PICK UP BUTTON ////
    public GameObject itemPickUp()
    {
        items = GameObject.FindGameObjectsWithTag("QuestItem");

        foreach (GameObject item in items)
        {
            if (Vector3.Distance(item.transform.position, thisRigidBody.transform.position) < 0.25f)
            {
                //Debug.Log(item);
                obj = item;
                pickUp.SetActive(true);
                break;
            }
            else
            {
                pickUp.SetActive(false);
                //Debug.Log(item);
            }
        }
        if (items.Length == 0)
            pickUp.SetActive(false);

        return obj;
    }

    //// WHEN TO ENABLE/DISABLE UP/DOWN BUTTONS ////
    public void ladderButtons()
    {
        Up = GameObject.FindGameObjectsWithTag("Up");
        Down = GameObject.FindGameObjectsWithTag("Down");


        foreach (GameObject up in Up)
        {
            if (Vector3.Distance(up.transform.position, thisRigidBody.transform.position) < 0.40f)
            {
                upIcon.SetActive(true);
                break;
            }
            else
                upIcon.SetActive(false);
        }

        foreach (GameObject down in Down)
        {
            if (Vector3.Distance(down.transform.position, thisRigidBody.transform.position) < 0.40f)
            {
                downIcon.SetActive(true);
                break;
            }
            else
                downIcon.SetActive(false);
        }
    }
   
}
