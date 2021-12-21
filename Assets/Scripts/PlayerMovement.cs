using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public static Vector3 moveDirection;

    private float playerSpeed = 5f;
    [SerializeField]private float jumpHeight = 1.0f;
    public Transform playerHead;

    public float turnSpeed = 15f;
    public static PlayerMovement playerMovement;
    private float x,y;
    private float cameraYaxis;
    
    public static Vector3 moveDir;
    public bool isJumping = false;
    bool running = false;
    bool jumped;
    Animator playerAnimator;
    PlayerScript playerScript;
    [SerializeField]private float _dashForce = 10f; 
    void Awake()
    {

        playerMovement = this;
    }
    private void Start() 
    {
        
        playerScript = GetComponent<PlayerScript>();
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    
    void Update()
    {
        
        //PLAYER INPUT DETECTION
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x,0,y).normalized;
        playerAnimator.SetFloat("moveX",moveDir.x);
        playerAnimator.SetFloat("moveZ",moveDir.z);
        
        cameraYaxis = Camera.main.transform.rotation.eulerAngles.y;

        //DETECT IF SPACE IS PRESSED
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            playerAnimator.SetBool("Jump",true);
            rb.AddForce(rb.transform.up * jumpHeight ,ForceMode.Impulse);
            Debug.Log("Jump Pressed");
            isJumping = true;
        }
        
        if(Input.GetKey(KeyCode.LeftShift))
        {
            
            playerSpeed = 15f;
        }else{
            playerSpeed = 5f;
        }
    }
    void FixedUpdate()
    {
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,cameraYaxis,0), turnSpeed * Time.fixedDeltaTime);
        
        if (moveDir.magnitude > 0)
        {
            playerAnimator.SetBool("isRunning",true);
            
            moveDirection = Quaternion.Euler(0,cameraYaxis,0)*moveDir;

            rb.MovePosition(rb.transform.position += moveDirection * playerSpeed * Time.deltaTime);
        }else{
            playerAnimator.SetBool("isRunning",false);
        }
       
    }
    //DETECT THE PLAYER'S COLLISION
   private void OnCollisionEnter(Collision other) {
       
        if(other.collider.tag == "Floor"){
            Debug.Log("Character Touching Floor");
            isJumping = false;
            playerAnimator.SetBool("Jump",false);
        }
    }
  
    
}
