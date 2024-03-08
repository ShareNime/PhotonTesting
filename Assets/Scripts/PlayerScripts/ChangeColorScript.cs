using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeColorScript : MonoBehaviourPunCallbacks
{
    [SerializeField] private Renderer mats;
    [SerializeField] private Material redmats;
    [SerializeField] private PhotonView view;
    [SerializeField] float red;
    [SerializeField] float green;
    [SerializeField] float blue;
    [SerializeField]  ChangeColorScript aaa;

    // Start is called before the first frame update
    private void Start() {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            red = UnityEngine.Random.Range(0,226/255f);
            green = UnityEngine.Random.Range(0,226/255f);
            blue = UnityEngine.Random.Range(0,226/255f);
            if(view.IsMine){
                view.RPC("ChangeColor", RpcTarget.AllBuffered, red,green,blue);
            }
        }
    }

    [PunRPC]
    private void ChangeColor(float r, float g, float b)
    {
        Debug.Log("color changed");
        // redmats.color = new Color(r,g,b);
        mats.material.color = new Color(r,g,b);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        

        base.OnPlayerEnteredRoom(newPlayer);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("WHY!");
        base.OnJoinedRoom();
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            red = UnityEngine.Random.Range(0,226/255f);
            green = UnityEngine.Random.Range(0,226/255f);
            blue = UnityEngine.Random.Range(0,226/255f);
            if(view.IsMine){
                view.RPC("ChangeColor", RpcTarget.AllBuffered, red,green,blue);
            }
        }
    }
}
