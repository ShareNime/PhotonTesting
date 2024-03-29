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
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using Photon.Voice;
public class PlayerController : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    [SerializeField] private bool _interactButtonHoldedVar = false;
    [SerializeField] private bool _jumpButtonPressed = false;
    [SerializeField] private float currSpeed;

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
    [SerializeField] GameObject AllGameObject;
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private GameObject _jumpButton;
    [SerializeField] private GameObject _interactButton;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        NickNameText.text = view.Owner.NickName;
        // NickNameText.text = "AAAA";
        currSpeed = Speed;
        if (!view.IsMine)
        {
            PlayerCameraObject.SetActive(false);
            gameObject.SetActive(false);
            PlayerCamera.enabled = false;
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            _joystick.gameObject.SetActive(true);
            _jumpButton.SetActive(true);
            Debug.Log("thisgame run on android");
        }
        // else
        // {
        //     _joystick.gameObject.SetActive(false);
        //     _jumpButton.SetActive(false);
        //     _interactButton.SetActive(false);
        //     Debug.Log("thisgame not run on android");
        // }
    }

    // Update is called once per frame
    void Update()
    {

        if (view.IsMine)
        {
            PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            if (Application.platform == RuntimePlatform.Android)
            {
                PlayerMovementInput = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
            }
            else
            {
                PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            }
            MovePlayer();
            if(Input.GetKeyDown(KeyCode.L)){
                AllGameObject.transform.position = new Vector3(502f,43f,480f);
            }
        }
    }
    void testdebug()
    {
        Debug.Log("TstDebug");
    }
    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);
        if (Controller.isGrounded)
        {
            Velocity.y = -1f;
            if (Input.GetKeyDown(KeyCode.Space) || _jumpButtonPressed)
            {
                Jump();
            }
        }
        else
        {
            Velocity.y -= Gravity * -2f * Time.deltaTime;
        }
        Controller.Move(currSpeed * Time.deltaTime * MoveVector);
        Controller.Move(Velocity * Time.deltaTime);
    }
    public void Jump()
    {
        Velocity.y = Jumpforce;
        Debug.Log("Plyaer Jump!");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovePlatform"))
        {
            PlayerInputView.RPC("AttachChild", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Grabable"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                _interactButton.SetActive(true);
            }
            if (Input.GetKey(KeyCode.F) || _interactButtonHoldedVar)
            {
                ConnectJointWithObject(other);
            }
            else
            {
                RemoveJointWithObject(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovePlatform"))
        {
            PlayerInputView.RPC("DetachChild", RpcTarget.All);
        }
        if (other.CompareTag("Grabable"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                _interactButton.SetActive(false);
            }
            // other.attachedRigidbody.isKinematic = true;
            PlayerInputView.RPC("JoinDisconnect", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
        }
        currSpeed = Speed;
    }
    public void JumpButtonPressed()
    {
        _jumpButtonPressed = true;
    }
    public void JumpButtonNotPressed()
    {
        _jumpButtonPressed = false;
    }
    public void InteractButtonHolded()
    {
        _interactButtonHoldedVar = true;
        // Debug.Log("InteractButtonHolded = " + _interactButtonHoldedVar);
    }
    public void InteractButtonNotHolded()
    {
        _interactButtonHoldedVar = false;
        // Debug.Log("InteractButtonHolded = " + _interactButtonHoldedVar);
    }
    private void ConnectJointWithObject(Collider other)
    {
        PlayerInputView.RPC("JointConnect", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
        currSpeed = GrabSpeed;
    }
    private void RemoveJointWithObject(Collider other)
    {
        PlayerInputView.RPC("JoinDisconnect", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
        // other.attachedRigidbody.isKinematic = true;
        currSpeed = Speed;
    }
    [PunRPC]
    void JointConnect(int targetView)
    {
        PhotonView.Find(targetView).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        GrabJoint.connectedBody = PhotonView.Find(targetView).gameObject.GetComponent<Rigidbody>();

    }

    [PunRPC]
    void JoinDisconnect(int targetView)
    {
        PhotonView.Find(targetView).gameObject.GetComponent<Rigidbody>().isKinematic = true;

        GrabJoint.connectedBody = null;
    }

    [PunRPC]
    private void AttachChild(int targetView)
    {
        AllGameObject.transform.SetParent(PhotonView.Find(targetView).gameObject.transform);
    }
    [PunRPC]
    private void DetachChild()
    {
        AllGameObject.transform.SetParent(null);
    }

}
