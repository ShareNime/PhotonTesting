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
    float[] rgb = new float[3];
    [SerializeField]  ChangeColorScript aaa;
    private Color _color;
    public Player player;
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    private void Start() {
        
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            if(!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("RandomColor")){
            red = UnityEngine.Random.Range(0,226/255f);
            green = UnityEngine.Random.Range(0,226/255f);
            blue = UnityEngine.Random.Range(0,226/255f);
            rgb = new float[] {red,green,blue};
            _myCustomProperties["RandomColor"] = rgb;
            PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
            Debug.Log("Color Generated = " + PhotonNetwork.LocalPlayer.CustomProperties["RandomColor"]);

        }else{
            
            Debug.Log("Color has been generated");
        }
            if(view.IsMine){
                // _color = (Color)PhotonNetwork.LocalPlayer.CustomProperties["RandomColor"];
                rgb = (float[]) PhotonNetwork.LocalPlayer.CustomProperties["RandomColor"];
                view.RPC("ChangeColor", RpcTarget.AllBuffered, rgb[0],rgb[1],rgb[2]);
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
}
