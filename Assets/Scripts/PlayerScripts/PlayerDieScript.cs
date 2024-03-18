using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerDieScript : MonoBehaviour
{
    [SerializeField] private PhotonView view;
    public int PlayerMaxHealth = 100;
    public int CurrPlayerHealth;
    private bool _isDead = false;
    public Vector3 SpawnPos;
    private float _timeToRespawn = 2f;
    private float _timeToRespawnCounter;
    [SerializeField] private GameObject _playerInputGameObject;
    [SerializeField] private GameObject _playerAvatarGameObject;
    // Start is called before the first frame update
    void Start()
    {
        CurrPlayerHealth = PlayerMaxHealth;
        _timeToRespawnCounter = _timeToRespawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine){
                if(CurrPlayerHealth <= 0){
                Debug.Log("Player Die");
                view.RPC("PlayerDie",RpcTarget.All);
            }
            if(_isDead){
                _timeToRespawnCounter -= Time.deltaTime;
            }   
            if(_timeToRespawnCounter <= 0){
                view.RPC("PlayerRespawn", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    void PlayerDie(){
        _isDead = true;
        _playerInputGameObject.SetActive(false);
        _playerAvatarGameObject.SetActive(false);
    }
    [PunRPC]
    void PlayerRespawn(){
        _isDead = false;
        CurrPlayerHealth = PlayerMaxHealth;
        _playerInputGameObject.SetActive(true);
        _playerAvatarGameObject.SetActive(true);
        _timeToRespawnCounter = _timeToRespawn;
        gameObject.transform.position = SpawnPos;
    }
}
