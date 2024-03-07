using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LobbyPlayerSpawner : MonoBehaviour
{
    [SerializeField] Transform posSpawn;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("PlayerNew", new Vector3(Random.Range(posSpawn.position.x, posSpawn.position.x + 20), posSpawn.position.y, Random.Range(posSpawn.position.z, posSpawn.position.z + 20)), Quaternion.identity);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
