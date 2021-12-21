using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyFiringScript : MonoBehaviour
{
    FieldOfView fov;
    private EnemyMovement enemyMovement;
    public event EventHandler EnemyCanShoot;
    public float timer = 0;
    public float delaySeconds = 1.5f;
    [SerializeField]private GameObject bulletPrefab;
    [SerializeField]private Transform firePoint;
    private void Start() {
        enemyMovement = GetComponent<EnemyMovement>();
        fov = GetComponent<FieldOfView>();
        enemyMovement.EnemyReadyToShoot += Shoot;
    }
    private void Shoot(object s, EventArgs e)
    {
        
        if (timer <= 0)
        {
            GameObject bulletInst = Instantiate(bulletPrefab,firePoint.position,Quaternion.identity);
            timer = delaySeconds;
            
        }else{
            timer -= Time.deltaTime;
        }
    }

}
