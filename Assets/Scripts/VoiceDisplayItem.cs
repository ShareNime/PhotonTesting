using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class VoiceDisplayItem : MonoBehaviour
{
    [SerializeField] TMP_Text _voiceDisplayText;
    private Player player;
    public void SetUp(string _playerName){
        // player = _player;
        _voiceDisplayText.text = _playerName;
        Debug.Log("Voice Display setup: " + _playerName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
