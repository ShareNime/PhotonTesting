using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;
public class LobbyPortalScript : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text textCountDown;
    [SerializeField] List<Collider> Players = new List<Collider>();
    private float timeToTeleport = 3f;
    [SerializeField] private float CountDownTimer;
    PhotonView view;
    bool isLoading = false;
    bool isCountDown = false;

    private void Start() {
        PhotonNetwork.AutomaticallySyncScene = true;
        view = GetComponent<PhotonView>();
        CountDownTimer = timeToTeleport;
    }
    private void Update() {
        textCountDown.text = CountDownTimer.ToString("0");
        if(PhotonNetwork.IsMasterClient){

            Teleport();
        }
        if(isCountDown){
            CountDown();
        }else{
            CountDownTimer = timeToTeleport;
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log(Players.Count);
        if(other.gameObject.CompareTag("Player")){
            Players.Add(other);
            Debug.Log("HAHAHAHA");
            Debug.Log(Players.Count);
        }
        if(Players.Count == 2 && PhotonNetwork.IsMasterClient){
            view.RPC("CheckCountDown", RpcTarget.All, true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            Players.Remove(other);
            Debug.Log(Players.Count);
        }
        if(PhotonNetwork.IsMasterClient){
            view.RPC("CheckCountDown", RpcTarget.All, false);
        }
    }
    // private void OnCollisionEnter(Collision other) {
    //     Debug.Log(Players.Count);
    //     if(other.gameObject.CompareTag("Player")){
    //         Players.Add(other.collider);
    //         Debug.Log("HAHAHAHA");
    //         Debug.Log(Players.Count);
    //     }
    //     if(Players.Count == 2 && PhotonNetwork.IsMasterClient){
    //         view.RPC("CheckCountDown", RpcTarget.All, true);
    //     }
    // }
    // private void OnCollisionExit(Collision other) {
    //     if(other.gameObject.CompareTag("Player")){
    //         Players.Remove(other.collider);
    //         Debug.Log(Players.Count);
    //     }
    //     if(PhotonNetwork.IsMasterClient){
    //         view.RPC("CheckCountDown", RpcTarget.All, false);
    //     }
    // }
    [PunRPC]
    void CheckCountDown(bool value){
        isCountDown = value;
    }
    void CountDown(){
        if(CountDownTimer > 0 && Players.Count == 2){
            CountDownTimer -= Time.deltaTime;
        }
    }
    void Teleport(){
        // if(CountDownTimer > 0 && PhotonNetwork.IsMasterClient){
        //     view.RPC("CountDown", RpcTarget.All);
        // }
        if(CountDownTimer <= 0 && PhotonNetwork.IsMasterClient  && !isLoading){
            Debug.LogWarning("INI MASTER");
            PhotonNetwork.LoadLevel("Level1");
            isLoading = true;
            // Destroy(this);
        }
    }
}
