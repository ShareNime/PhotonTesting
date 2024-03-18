using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Pun;
public class PushToTalk : MonoBehaviour
{
    private Recorder _recorder;
    [SerializeField] private GameObject[] _micIndicatorGameobjects;

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
        if(Input.GetKeyDown(KeyCode.G)){
        _recorder.TransmitEnabled = !_recorder.TransmitEnabled;
        if(_recorder.TransmitEnabled){
            _micIndicatorGameobjects[0].SetActive(true);
            _micIndicatorGameobjects[1].SetActive(false);
        }else{
            _micIndicatorGameobjects[1].SetActive(true);
            _micIndicatorGameobjects[0].SetActive(false);
        }
        Debug.Log("transmitEnabled = " + _recorder.TransmitEnabled);
        }
    }
}
