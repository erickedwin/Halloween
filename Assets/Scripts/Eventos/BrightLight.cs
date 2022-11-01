using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightLight : MonoBehaviour
{
    public AudioSource rompe;
    public Light luz_parpadenate;
    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            luz_parpadenate.enabled = false;
            rompe.volume = 0.7f;
            rompe.Play();
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
