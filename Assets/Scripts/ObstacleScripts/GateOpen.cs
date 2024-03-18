using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    [SerializeField] private int _triggerToOpen;
    [SerializeField] private FloorTrigger[] _floorTrigger;
    [SerializeField] private Material _material;
    public bool IsOpen = false;
    [SerializeField] private Vector3 _targetPos;
    [SerializeField] private float _speed = 10f;
    private Vector3 _startPos;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(_triggerToOpen == 1){
            if(_floorTrigger[0].IsTriggered){
                OpenGate();
            }else{
                // GateClose();
            }
        }
        if(_triggerToOpen == 2){
            if(_floorTrigger[0].IsTriggered && _floorTrigger[1].IsTriggered){
                OpenGate();
            }else{
                // GateClose();
            }
        }
    }
    private void OpenGate(){
        transform.position = Vector3.Lerp(transform.position, _targetPos, _speed * Time.deltaTime);
        IsOpen = true;
        _material.color = Color.black;
    }
    private void GateClose(){
        IsOpen = false;
        transform.position = Vector3.Lerp(transform.position, _startPos, _speed * Time.deltaTime);
        _material.color = Color.green;
    }
}
