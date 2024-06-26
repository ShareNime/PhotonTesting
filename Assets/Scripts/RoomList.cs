using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject RoomPrefab;
    public GameObject[] AllRooms;
    private void Start() {
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        RefreshRoomList(roomList);
    }
    public void RefreshRoomList(List<RoomInfo> roomList){
        for (int i = 0; i < AllRooms.Length; i++)
        {
            if(AllRooms[i] != null){
                Destroy(AllRooms[i]);
            }
        }
        AllRooms = new GameObject[roomList.Count];
        for(int i = 0; i < roomList.Count; i++){
            if(roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1){
                print(roomList[i].Name);
                GameObject Room = Instantiate(RoomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                Room.GetComponent<Room>().Name.text = roomList[i].Name;
                Room.GetComponent<Room>().RoomPlayers.text = "| Player: " + roomList[i].PlayerCount.ToString() + "/" + roomList[i].MaxPlayers.ToString();
                AllRooms[i] = Room;
            }
        }
    }
}
