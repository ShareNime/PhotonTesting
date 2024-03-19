using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBallSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _spawnTimeCounter;
    [SerializeField] private GameObject _rollingBallObject;
    private void Start() {
        _spawnTimeCounter = _spawnTime;
        foreach(Transform transform in _spawnPoints){
            transform.position = transform.localPosition;
        }
    }
    private void Update() {
        if(_spawnTimeCounter <= 0){
            int i = Random.Range(0,4);
            Debug.Log(i);
            Instantiate(_rollingBallObject,_spawnPoints[i]);
            _spawnTimeCounter = _spawnTime;
        }else{
            _spawnTimeCounter-=Time.deltaTime;
        }
    }
}
