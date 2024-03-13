using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ObjectMoveTest : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(10f,0,0);
    }
}
