using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Voice;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity.Demos.DemoVoiceUI;
public class VoiceDisplay : MonoBehaviour
{
    [SerializeField] private PhotonView _pv;
    [SerializeField] private PhotonVoiceView _pvv;
    [SerializeField] private GameObject _voiceIndicatorGameobject;
    // Start is called before the first frame update
    void Start()
    {
        Player[] players = PhotonNetwork.PlayerList;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        _voiceIndicatorGameobject.SetActive(this._pvv.IsSpeaking);
        // if(this._pvv.IsSpeaking){
        //     _voiceIndicator.SetActive(true);
        // }else{
        //     _voiceIndicator.SetActive(false);

        //     // Debug.Log(PhotonNetwork.LocalPlayer.NickName + "is not Speaking");
        // }
        
    }
}