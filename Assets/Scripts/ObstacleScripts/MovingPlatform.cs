using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3[] _waypoints;
    [SerializeField] private float _moveSpeed;
    private int _currentTargetIndex = 0;
    private float _distanceToTarget;
    private PhotonView view;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }
    private void MovePlatform(){
        _distanceToTarget = Vector3.Distance(transform.position, _waypoints[_currentTargetIndex]);
        if(_distanceToTarget < 0.1){
            _currentTargetIndex++;
            if(_currentTargetIndex >= _waypoints.Length){
                _currentTargetIndex = 0;
            }
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentTargetIndex], _moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            // other.transform.SetParent(transform);
            // view.RPC("AttachChild",RpcTarget.All,other.gameObject.GetPhotonView().ViewID);
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            // other.transform.SetParent(null);
            // view.RPC("DetachChild",RpcTarget.All,other.gameObject.GetPhotonView().ViewID);
        }
    }
    [PunRPC]
    private void AttachChild(int targetView){
        PhotonView.Find(targetView).gameObject.transform.SetParent(transform);
    }
    [PunRPC]
    private void DetachChild(int targetView){
        PhotonView.Find(targetView).gameObject.transform.SetParent(null);
    }
}