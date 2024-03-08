using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class LobbyPlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform posSpawn;
    [SerializeField] private Renderer mats;
    [SerializeField] private Material redmats;
    
    private void Awake() {
        PhotonNetwork.Instantiate("PlayerNew 1", new Vector3(Random.Range(posSpawn.position.x, posSpawn.position.x + 20), posSpawn.position.y, Random.Range(posSpawn.position.z, posSpawn.position.z + 20)), Quaternion.identity);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        
        Debug.Log(otherPlayer.NickName + " has left the game");
    }
}
