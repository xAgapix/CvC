using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocatorScript : MonoBehaviour
{
    public event EventHandler LocatorCollided;
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Locator Collided");
        if ( other.transform.tag == "Wall" || other.transform.tag == "Floor")
        {
            LocatorCollided?.Invoke(this, EventArgs.Empty);
        }    
    }
}
