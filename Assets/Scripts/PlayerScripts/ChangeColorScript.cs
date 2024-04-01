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
    [SerializeField] private Renderer _avatarMats;
    [SerializeField] private Renderer _pointMats;
    [SerializeField] private Material _redMats;
    [SerializeField] private PhotonView _view;
    [SerializeField] float _red;
    [SerializeField] float _green;
    [SerializeField] float _blue;
    private float[] _rgb = new float[3];
    [SerializeField] ChangeColorScript _aaa;
    private Color _color;
    public Player Player;
    private ExitGames.Client.Photon.Hashtable _myColorCustomProperties = new ExitGames.Client.Photon.Hashtable();

    // Start is called before the first frame update
    private void Start()
    {

        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("RandomColor"))
            {
                _red = UnityEngine.Random.Range(0, 226 / 255f);
                _green = UnityEngine.Random.Range(0, 226 / 255f);
                _blue = UnityEngine.Random.Range(0, 226 / 255f);
                _rgb = new float[] { _red, _green, _blue };
                _myColorCustomProperties.Add("RandomColor", _rgb);
                PhotonNetwork.LocalPlayer.SetCustomProperties(_myColorCustomProperties);
                if (_view.IsMine)
                {
                    _view.RPC("ChangeColor", RpcTarget.AllBuffered, _rgb[0], _rgb[1], _rgb[2]);
                }
            }
            else
            {
                Debug.Log("Color has been generated");
                if (_view.IsMine)
                {
                    if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("RandomColor", out object rgbColor))
                    {
                        _rgb = (float[])rgbColor;
                        _view.RPC("ChangeColor", RpcTarget.AllBuffered, _rgb[0], _rgb[1], _rgb[2]);
                    }
                    else
                    {
                        Debug.Log("Failed to Get Color");
                    }
                }
            }
            Debug.Log("Color Generated = " + PhotonNetwork.LocalPlayer.CustomProperties["RandomColor"]);
        }
    }

    [PunRPC]
    private void ChangeColor(float r, float g, float b)
    {
        Debug.Log("color changed");
        _avatarMats.material.color = new Color(r, g, b);
        _pointMats.material.color = new Color(r,g,b);
    }
}
