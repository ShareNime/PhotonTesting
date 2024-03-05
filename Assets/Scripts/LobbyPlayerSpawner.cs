using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LobbyPlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("Player",new Vector3(Random.Range(500f, 550f), 1.25f, Random.Range(150f, 160f)), Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
