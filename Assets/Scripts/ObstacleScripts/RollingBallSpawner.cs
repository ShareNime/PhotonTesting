using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RollingBallSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _spawnTimeCounter;
    [SerializeField] private GameObject _rollingBallObject;
    [SerializeField] PhotonView _view;
    private int _randomPoint;
    private void Start()
    {
        _spawnTimeCounter = _spawnTime;
    }
    private void Update()
    {

        if (_spawnTimeCounter <= 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                _randomPoint = Random.Range(0, 4);
                _view.RPC("SendRandomSpawnPoint", RpcTarget.MasterClient, _randomPoint);
            }
            _spawnTimeCounter = _spawnTime;
        }
        else
        {
            _spawnTimeCounter -= Time.deltaTime;
        }
    }
    [PunRPC]
    void SendRandomSpawnPoint(int _index)
    {
        PhotonNetwork.Instantiate("RollingBall", _spawnPoints[_index].position, Quaternion.identity);
    }
}
