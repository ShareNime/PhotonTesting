using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Realtime;
using Unity.VisualScripting;
using Photon.Voice;
public class PushToTalk : MonoBehaviourPunCallbacks
{
    private Recorder _recorder;
    [SerializeField] private GameObject[] _micIndicatorGameobjects;
    [SerializeField] private bool _micButtonPressed = false;
    public static PushToTalk instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _recorder = GetComponent<Recorder>();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    private void EnableTalking()
    {
        _recorder.TransmitEnabled = true;
    }
    private void DisableTalking()
    {
        _recorder.TransmitEnabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _recorder.TransmitEnabled = !_recorder.TransmitEnabled;
            if (_recorder.TransmitEnabled)
            {
                _micIndicatorGameobjects[0].SetActive(true);
                _micIndicatorGameobjects[1].SetActive(false);
            }
            else
            {
                _micIndicatorGameobjects[1].SetActive(true);
                _micIndicatorGameobjects[0].SetActive(false);
            }
            Debug.Log("transmitEnabled = " + _recorder.TransmitEnabled);
        }
    }
    public void MicButtonClicked()
    {
        _recorder.TransmitEnabled = !_recorder.TransmitEnabled;
        if (_recorder.TransmitEnabled)
        {
            _micIndicatorGameobjects[0].SetActive(true);
            _micIndicatorGameobjects[1].SetActive(false);
        }
        else
        {
            _micIndicatorGameobjects[1].SetActive(true);
            _micIndicatorGameobjects[0].SetActive(false);
        }
    }
    public void MicButtonNotPressed()
    {
        _micButtonPressed = false;
    }
}
