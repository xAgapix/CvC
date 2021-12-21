using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerScript : MonoBehaviour
{
    public event EventHandler Skill1;
    public event EventHandler Skill2;
    public event EventHandler Skill3;
    public event EventHandler Dash;
    bool attackCast = false;
    [SerializeField]private LayerMask targetMask;
    [SerializeField]private GameObject playerWeapon;

    [SerializeField]private float dashForce = 10f;
    private Rigidbody rb;
    Vector3 hitVec,weaponPos;
    Transform weaponTransform;
    [SerializeField]private Transform weaponHolderTrans;
    bool reached = false;
    
    private void Start() {
        
        
        rb = GetComponent<Rigidbody>();
        weaponTransform = playerWeapon.GetComponent<Transform>();
        
    }
    
    void Update()
    {
        if(!attackCast){weaponTransform.position = weaponHolderTrans.position;}
        
        BasicAttack();
            
            if (Input.GetKeyUp(KeyCode.C))
            {
                Dash?.Invoke(this,EventArgs.Empty);
                //Dash Skill
            }

    }
    
    private void BasicAttack(){
        Vector2 screenCenter = new Vector2(Screen.width/2,Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit raycastHit;
        
        
        
        
        if (Input.GetButtonDown("Fire1") && !attackCast)
            {
                
                if (Physics.Raycast(ray, out raycastHit, targetMask))
                {
                    if(raycastHit.collider.tag != "Player")
                    {
                        Debug.Log(raycastHit.collider.name);
                        hitVec = raycastHit.point;
                        attackCast = true;
                    }
                    
                }
                
            }
    
    //BOOMERANG BASIC ATTACK
        if(attackCast)
        {
            float distance = Vector3.Distance(weaponTransform.position,hitVec);
            float range = Vector3.Distance(weaponTransform.position,weaponHolderTrans.position);
            transform.position = Vector3.MoveTowards(transform.position,hitVec,10f*Time.deltaTime);
            
            if (distance <= 0 || reached || range >= 10f){
                reached = true;
                weaponTransform.position = Vector3.MoveTowards(weaponTransform.position,weaponHolderTrans.position,30f*Time.deltaTime);
                
                if (weaponTransform.position == weaponHolderTrans.position)
                {
                attackCast = false;
                reached = false;
                }
            
            }
            if (!reached){
                weaponTransform.position = Vector3.MoveTowards(weaponTransform.position,hitVec,20f*Time.deltaTime);
            }
            
            
        }
    }
}

