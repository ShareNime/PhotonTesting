using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public bool IsTriggered = false;
    public GateOpen TriggeredGate;
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
            if(TriggeredGate.IsOpen){
                other.gameObject.GetComponent<PlayerDieScript>().SpawnPos = gameObject.transform.position;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            IsTriggered = false;
        }
    }
}
