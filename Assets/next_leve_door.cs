using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class next_leve_door : MonoBehaviour
{
   private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            SceneManager.LoadScene("Level2", LoadSceneMode.Additive);
        }
    }
}
