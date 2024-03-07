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

    // Start is called before the first frame update
    void Start()
    {
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
        Cursor.lockState = CursorLockMode.Locked;   
        if(view.IsMine){
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
        Controller.Move(currSpeed * Time.deltaTime * MoveVector);
        Controller.Move(Velocity * Time.deltaTime);
    }
    private void MovePlayerCamera(){
        // // Vector3 viewDir = transform.position - new Vector3(PlayerCamera.position.x, transform.position.y, PlayerCamera.position.z );
        // Vector3 viewDir = transform.position - new Vector3(PlayerCamera.position.x, transform.position.y, PlayerCamera.position.z );
        // followTransfrom.forward = viewDir.normalized;

        // Vector3 inputDir = followTransfrom.forward * Input.GetAxis("Vertical") + followTransfrom.right * Input.GetAxis("Horizontal");
        

        // if(inputDir != Vector3.zero){
        //     transform.forward = Vector3.Slerp(transform.forward, inputDir.normalized, Time.deltaTime * Sensitivity);
        //     transform.forward = inputDir.normalized;
        //     // PlayerCamera.Rotate(0,0,0);
        // }
        // // PlayerCamera.localEulerAngles = new Vector3(PlayerMouseInput.y, followTransfrom.position.y, followTransfrom.position.z);
        // // transform.eulerAngles = new Vector3(transform.eulerAngles.x, PlayerMouseInput.x,transform.eulerAngles.z);
    }
    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("Grabable")){
            if(Input.GetKey(KeyCode.F)){
                // if(other.GetComponent<PhotonView>().Owner == PhotonNetwork.LocalPlayer){
                    GrabJoint.connectedBody = other.attachedRigidbody;
                    other.attachedRigidbody.isKinematic = false;
                // }else{
                //     other.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
                // }
                currSpeed = GrabSpeed;
            }else{
                GrabJoint.connectedBody = null;
                other.attachedRigidbody.isKinematic = true;
                currSpeed = Speed;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Grabable")){
            other.attachedRigidbody.isKinematic = true;
        }
        currSpeed = Speed;
        GrabJoint.connectedBody = null;
    }
}
