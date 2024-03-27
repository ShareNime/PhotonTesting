using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CrazyBallScript : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _randomMoveTimer = 2f;
    [SerializeField] private float _randomMoveTimerCounter;
    private float _axisX;
    private float _axisY;
    private float _axisZ;

    // Start is called before the first frame update
    void Start()
    {
        _randomMoveTimerCounter = _randomMoveTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(_randomMoveTimerCounter <= 0){
            if(PhotonNetwork.IsMasterClient){
                _axisX = Random.Range(-5,5);
                _axisZ = Random.Range(-5,5);
            }
            _randomMoveTimerCounter = _randomMoveTimer;
        }else{
                _rb.AddForce(_axisX, 0f, _axisZ);
            _randomMoveTimerCounter -= Time.deltaTime;
        }
    }
    
    
}
