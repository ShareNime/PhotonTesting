using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CollectibleItemScript : MonoBehaviour
{
    [SerializeField] PhotonView _view;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50f * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            _view.RPC("DestroyItem",RpcTarget.MasterClient);
        }
    }
    [PunRPC]
    void DestroyItem(){
        PhotonNetwork.Destroy(gameObject);
    }
}
