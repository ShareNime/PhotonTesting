using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerListManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] Transform playerListContent;
    private List<GameObject> PlayerListPrefabsInstantiate;
    private void Start() {
        Player[] players = PhotonNetwork.PlayerList;
        for(int i = 0; i < players.Count(); i++){
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        Player[] players = PhotonNetwork.PlayerList;
        for(int i = 0; i < players.Count(); i++){
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }
}
