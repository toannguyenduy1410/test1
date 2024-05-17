using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efectss : MonoBehaviour
{
    //public ParticleSystem paticaleEfects;
    public ParticleSystem particleSystem;
    public ParticleSystem particleSystem2;
    void Start()
    {
        //particleSystem = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("player"))
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
                particleSystem2.Play();
            }
        }
    }
        
}
