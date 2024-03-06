using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
public class Testing : MonoBehaviour
{
    // [SerializeField] PhotonView viewText;
    [SerializeField] TMP_Text triggerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangeText(string text){
        triggerText.text = text;
    }
    [PunRPC]
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Grabable")){
            ChangeText("DONE");
        }
    }
}
