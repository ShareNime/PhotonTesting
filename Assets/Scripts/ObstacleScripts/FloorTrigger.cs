using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public bool IsTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Player")){
            IsTriggered = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            IsTriggered = false;
        }
    }
}
