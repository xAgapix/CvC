using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField]private Transform[] patrol_Locator;
    public event EventHandler EnemyReadyToShoot;
    public float moveSpeed = 5f; 
    public float timer;
    public float delaySeconds;
    NavMeshAgent agent;
    private Vector3 enemyNextWaypoint;
    FieldOfView fovScript;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        fovScript = GetComponent<FieldOfView>();
        
    }
    void Start()
    {
        
        timer = delaySeconds;
        InvokeRepeating("RelocatePatrolLocator",1f,10f);
        
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position,enemyNextWaypoint);
        if(distance <= 2f)
        {
        RelocatePatrolLocator();
        }
        PlayerFound();
        
        
    }
    public void PlayerFound()
    {
        if (fovScript.canSeePlayer)
        {
            if (timer <= 0)
            {
                
                
                fovScript.radius = 20f;
                fovScript.angle = 270f;
                agent.SetDestination(fovScript.target.position);
                agent.speed = 5f;
                EnemyReadyToShoot?.Invoke(this, EventArgs.Empty);
                
            }else{
                timer -= Time.deltaTime;
            }
            
        }else{
            timer = delaySeconds;
            fovScript.radius = 15f;
            fovScript.angle = 150f;
            agent.SetDestination(enemyNextWaypoint);
            agent.speed = 3f;
        }
    }

    private void RelocatePatrolLocator()
    {
        
        
            int wayPointRandomizer = UnityEngine.Random.Range(0,3);
            enemyNextWaypoint = patrol_Locator[wayPointRandomizer].position;
        
    }
}
