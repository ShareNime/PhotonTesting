using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Pun;
public class PushToTalk : MonoBehaviour
{
    private Recorder _recorder;
    private void Awake() {
        if(_recorder == null){
            _recorder = GetComponent<Recorder>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void EnableTalking(){
        _recorder.TransmitEnabled = true;
    }
    private void DisableTalking(){
        _recorder.TransmitEnabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.G)){
            EnableTalking();
        }else if(Input.GetKeyUp(KeyCode.G)){
            DisableTalking();
        }
    }
}
