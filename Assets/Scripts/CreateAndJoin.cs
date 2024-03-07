using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField input_create;
    public TMP_InputField input_join;
    public int RoomIDCounter;
    public void CreateRoom(){
        PhotonNetwork.CreateRoom(input_create.text, new RoomOptions(){MaxPlayers = 2}, TypedLobby.Default, null);
    }
    public void JoinRoom(){
        PhotonNetwork.JoinRoom(input_join.text);
    }
    public void JoinRoomInList(string RoomName){
        PhotonNetwork.JoinRoom(RoomName);
    }
    public void MatchMaking(){
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(System.Guid.NewGuid().ToString()[..3], new RoomOptions(){MaxPlayers = 2}, TypedLobby.Default, null);
        base.OnJoinRandomFailed(returnCode, message);
    }
    public override void OnJoinedRoom(){
        PhotonNetwork.LoadLevel("InGame");
        Debug.Log(PhotonNetwork.CountOfPlayersInRooms);
    }
}
