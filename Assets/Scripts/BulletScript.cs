using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    [HideInInspector]public Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        transform.LookAt(playerTransform.position);

        Vector3 aimShoot = playerTransform.position - transform.position;
        rb.AddForce(transform.forward * 10f,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Bullet collided");
       Destroy(this.gameObject);
    }
}
