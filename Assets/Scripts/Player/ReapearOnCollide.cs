using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReapearOnCollide : MonoBehaviour
{
    [SerializeField]
    Transform newLocation;

    CharacterController controller;

    int deaths;

    void Start()
    {
        //Cambialo si quieres.
        deaths = 0;
        controller = GetComponent<CharacterController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Definido");
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            print("Muerto");
            //transform.position = newLocation.localPosition;
            controller.Move(newLocation.localPosition - gameObject.transform.position);
            deaths++;
        }
    }
}
