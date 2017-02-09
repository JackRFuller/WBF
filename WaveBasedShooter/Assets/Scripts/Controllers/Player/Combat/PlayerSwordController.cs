using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordController : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            Debug.Log("Hit Enemy");
        }
    }
}
