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
using TMPro;
public class PlayerController : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    [SerializeField] private float currSpeed;

    private float xRot;
    [SerializeField] private FixedJoint GrabJoint;
    [SerializeField] private Transform followTransfrom;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private PlayerCam PlayerCamera;
    [SerializeField] private GameObject PlayerCameraObject;
    [SerializeField] private float Speed;
    [SerializeField] private float GrabSpeed;
    [SerializeField] private float Jumpforce;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Gravity = -9.81f;
    [SerializeField] private PhotonView view;
    [SerializeField] private TMP_Text NickNameText;
    [SerializeField] private PhotonView PlayerInputView;
    private bool IsConnected = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        NickNameText.text = view.Owner.NickName;
        currSpeed = Speed;
        if(!view.IsMine){
            PlayerCameraObject.SetActive(false);
            gameObject.SetActive(false);
            PlayerCamera.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(view.IsMine){
            PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            MovePlayer();
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
        Controller.Move(currSpeed * Time.deltaTime * MoveVector);
        Controller.Move(Velocity * Time.deltaTime);
    }
    private void OnTriggerStay(Collider other) {
        // if(PlayerInputView.IsMine){

            if(other.CompareTag("Grabable")){

            if(Input.GetKey(KeyCode.F)){
                    // GrabJoint.connectedBody = other.attachedRigidbody;
                    PlayerInputView.RPC("JointConnect",RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
                // if(other.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer){
                    Debug.Log("This Player ATtach joint");
                    other.attachedRigidbody.isKinematic = false;
                    // PlayerInputView.RPC("JointConnect",RpcTarget.All); 
                // }else{
                //     other.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                // }
                currSpeed = GrabSpeed;
            }else{
                // PlayerInputView.RPC("JoinDisconnect",RpcTarget.All,other);
                PlayerInputView.RPC("JoinDisconnect",RpcTarget.All);
                other.attachedRigidbody.isKinematic = true;
                currSpeed = Speed;
            }
        }
        // }
        
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Grabable")){
            other.attachedRigidbody.isKinematic = true;
        }
        currSpeed = Speed;
        PlayerInputView.RPC("JoinDisconnect",RpcTarget.All);
    }
    [PunRPC]
    void JointConnect(int targetView){
        GrabJoint.connectedBody = PhotonView.Find(targetView).gameObject.GetComponent<Rigidbody>();
    }
    [PunRPC]
    void JoinDisconnect(){
        GrabJoint.connectedBody = null;
    }
}
