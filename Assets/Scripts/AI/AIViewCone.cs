using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIViewCone : MonoBehaviour
{
    [SerializeField]
    float radius;

    [SerializeField]
    float angle;

    [SerializeField]
    LayerMask playerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);
    }
}
