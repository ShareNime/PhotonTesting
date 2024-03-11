using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectAndDisconnectScript : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject MenuPanel;
    [SerializeField] bool isMenuOpen = false;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            // DisconnectPlayer();
            Cursor.lockState = CursorLockMode.None;
            PhotonNetwork.Disconnect();
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            isMenuOpen = !isMenuOpen;
            if(isMenuOpen){
                MenuPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }else{
                MenuPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void LeaveRoom(){
        Debug.Log("KELUAR WOI");
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
        base.OnLeftRoom();
    }
    // public void DisconnectPlayer(){
    //     StartCoroutine(DisconnectAndLoad());
    // }
    // IEnumerator DisconnectAndLoad(){
    //     // PhotonNetwork.Disconnect();

    //     while(PhotonNetwork.IsConnected){
    //         // yield return null;

    //     }
    // }
    public override void OnDisconnected(DisconnectCause cause)
    {
        
        base.OnDisconnected(cause);
        Debug.LogWarning("You have been disconnected");
        
        // PhotonNetwork.Reconnect();

        PhotonNetwork.LoadLevel("Loading");

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.LogWarning("Player Conneceted");
    }
}
