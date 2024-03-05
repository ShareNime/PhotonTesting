using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Photon.Realtime;
using Photon.Pun;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
public class PlayerController : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    [SerializeField] private FixedJoint GrabJoint;
    [SerializeField] private Transform followTransfrom;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private GameObject PlayerCameraObject;
    [SerializeField] private float Speed;
    [SerializeField] private float Jumpforce;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        if(!GetComponent<PhotonView>().IsMine){
            PlayerCameraObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(GetComponent<PhotonView>().IsMine){
            PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            MovePlayer();
            MovePlayerCamera();
        }
        
    }
    private void MovePlayer(){
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);
        if(Controller.isGrounded){
            Velocity.y = -1f;
            if(Input.GetKeyDown(KeyCode.Space)){
                Velocity.y = Jumpforce;
            }
        }else{
            Velocity.y -= Gravity * -2f * Time.deltaTime;
        }
        Controller.Move(Speed * Time.deltaTime * MoveVector);
        Controller.Move(Velocity * Time.deltaTime);
    }
    private void MovePlayerCamera(){
        Vector3 viewDir = transform.position - new Vector3(PlayerCamera.position.x, transform.position.y, PlayerCamera.position.z );
        followTransfrom.forward = viewDir.normalized;

        Vector3 inputDir = followTransfrom.forward * PlayerMovementInput.z + followTransfrom.right * PlayerMovementInput.x;
        if(inputDir != Vector3.zero){
            transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * Sensitivity);
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Grabable")){
            if(Input.GetKey(KeyCode.F)){
                if(other.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer){
                    GrabJoint.connectedBody = other.attachedRigidbody;
                    other.attachedRigidbody.isKinematic = false;
                }else{
                    other.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                }
            }else{
                GrabJoint.connectedBody = null;
                other.attachedRigidbody.isKinematic = true;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Grabable")){
            other.attachedRigidbody.isKinematic = true;
        }
        GrabJoint.connectedBody = null;
    }
}
