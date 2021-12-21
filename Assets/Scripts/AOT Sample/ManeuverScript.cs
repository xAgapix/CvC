using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManeuverScript : MonoBehaviour
{
    [SerializeField]private LayerMask targetMask;
    [SerializeField]private float _hookSpeed = 2f;
    [SerializeField]private float _stopDistance = 2f;
    [SerializeField]private Transform hookHolder;
    [SerializeField]private Transform hook;

    private Rigidbody _rb;
    private Vector3 hitVec;
    public bool _wallHit = false;
    public bool hookArrived = false;
    Animator playerAnimator;
    private void Start() {
        _rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        
    }
    
    private void Update() {
        
        MouseAimHook();
        
    }
    private void FixedUpdate() {
        MoveHookToHitVec();
        MoveToHook();
        
        
    }
    private void MoveHookToHitVec()
    {
        if (_wallHit)
        {
            
            if(hook.position != hitVec)
            {
                hook.position = Vector3.MoveTowards(hook.position,hitVec, _hookSpeed*3 * Time.fixedDeltaTime);
            }else{
                
                hookArrived = true;
                _rb.isKinematic = true;
            }
            
            
        }else{
            
            hook.position = hookHolder.position;
        
        }
        
    }
    
    //MOVE TO Hook
    private void MoveToHook()
    {
        if (hookArrived)
        {
            
            playerAnimator.SetBool("Jump",true);
            float playerWallDistance = Vector3.Distance(transform.position,hitVec);
            if(playerWallDistance >= _stopDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,hitVec, _hookSpeed * Time.fixedDeltaTime);
            }else{
                playerAnimator.SetBool("Jump",false);
                hookArrived = false;
                _wallHit = false;
                _rb.isKinematic = false;
            }
            
        }
        
    }
    //MOUSE AIM
    private void MouseAimHook(){
        Vector2 screenCenter = new Vector2(Screen.width/2,Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit raycastHit;
        //MOUSE LEFT CLICK
        if (Input.GetKeyDown(KeyCode.C))
            {
                if (Physics.Raycast(ray, out raycastHit, targetMask))
                {
                    if(raycastHit.collider.name != "PLAYER")
                    {
                        
                        Debug.Log(raycastHit.collider.name);
                        hitVec = raycastHit.point;
                        _wallHit = true;
                    }
                    
                }
                
            }
    }
}