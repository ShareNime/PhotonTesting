using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGiveDamageScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerDieScript>().CurrPlayerHealth -= 100;
        }
    }
}
