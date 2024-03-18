using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using UnityEngine;

public class PlayerDieScript : MonoBehaviourPunCallbacks
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
    [SerializeField] int _deadCounter;
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    // Start is called before the first frame update
    void Start()
    {
        CurrPlayerHealth = PlayerMaxHealth;
        _timeToRespawnCounter = _timeToRespawn;
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            if(!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("DeadCounter")){
                _deadCounter = 0;
                // _myCustomProperties["DeadCounter"] = _deadCounter;
                // _myCustomProperties["DeadCounter"] = _deadCounter;
                _myCustomProperties.Add("DeadCounter",_deadCounter);
                PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);
                // PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
            }else{
                _deadCounter = (int) PhotonNetwork.LocalPlayer.CustomProperties["DeadCounter"];
                Debug.Log("DeadCounter Has Been Generated");
            }
        }
                Debug.Log("DeadCounter Generated = " + PhotonNetwork.LocalPlayer.CustomProperties["DeadCounter"]);

    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine){
            if(CurrPlayerHealth <= 0){
                Debug.Log("Player Die");
                view.RPC("PlayerDie",RpcTarget.All);
                view.RPC("AddDieCounter",view.Owner);
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
       
        // ("DeadCounter",_deadCounter);
        
        // _myCustomProperties["DeadCounter"] = _deadCounter;
        // PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
        Debug.Log(view.Owner.NickName + " Dead is " + (int) PhotonNetwork.LocalPlayer.CustomProperties["DeadCounter"]);
        CurrPlayerHealth = PlayerMaxHealth;
    }
    [PunRPC]
    void AddDieCounter(){
        _deadCounter++;
        _myCustomProperties["DeadCounter"] = _deadCounter;
        // PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);
    }
    [PunRPC]
    void PlayerRespawn(){
        _isDead = false;
        
        _playerInputGameObject.SetActive(true);
        _playerAvatarGameObject.SetActive(true);
        _timeToRespawnCounter = _timeToRespawn;
        gameObject.transform.position = SpawnPos;
    }
}
