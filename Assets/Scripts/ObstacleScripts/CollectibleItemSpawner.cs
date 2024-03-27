using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class CollectibleItemSpawner : MonoBehaviour
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private GateOpen _gateTrigger;
    [SerializeField] private int _maxSpawn = 20;
    [SerializeField] private int _numSpawned = 0;
    [SerializeField] private float _spawnDelay = 1f;
    private float _spawnDelayCounter;
    // Start is called before the first frame update
    void Start()
    {
        _spawnDelayCounter = _spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.IsMasterClient){
            if(_gateTrigger.IsOpen && _numSpawned < _maxSpawn){
                StartSpawning();
            }
        }
       
        if(Input.GetKeyDown(KeyCode.P)){
            SpawnObject();
        }
    }
    void SpawnObject(){
        Vector3 randomPos = Random.insideUnitSphere * _radius;
        float _xpos = randomPos.x;
        float _zpos = randomPos.z;
        PhotonNetwork.Instantiate("CoinCollect", new Vector3(_xpos, 40f,_zpos) + new Vector3(transform.position.x , 0f, transform.position.z), Quaternion.identity);
        _numSpawned++;
    }
    private void StartSpawning(){
        if(_spawnDelayCounter >= 0f){
            _spawnDelayCounter -= Time.deltaTime;
        }else{
            _spawnDelayCounter = _spawnDelay;
            SpawnObject();
        }
    }
}
