using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BallRolling : MonoBehaviour
{
    [SerializeField] float _timeToLive = 15f;
    private void Start()
    {
        // Destroy(gameObject, _timeToLive);
    }
    private void Update()
    {
        if (_timeToLive <= 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        else
        {
            _timeToLive -= Time.deltaTime;
        }

    }
}
