using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlKhay : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            other.gameObject.GetComponent<player>().RemoveBrick();            
        }
    }
}
