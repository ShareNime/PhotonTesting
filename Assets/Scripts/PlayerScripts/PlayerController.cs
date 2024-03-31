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
    private Vector3 _velocity;
    private Vector3 _playerMovementInput;
    private Vector2 _playerMouseInput;
    [SerializeField] private bool _interactButtonHoldedVar = false;
    [SerializeField] private bool _jumpButtonPressed = false;
    [SerializeField] private float _currSpeed;

    [SerializeField] private FixedJoint _grabJoint;
    [SerializeField] private Transform _followTransform;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private PlayerCam _playerCamera;
    [SerializeField] private GameObject PlayerCameraObject;
    [SerializeField] private float _speed;
    [SerializeField] private float _grabSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _sensitivity;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private PhotonView _view;
    [SerializeField] private TMP_Text __nicknameText;
    [SerializeField] private PhotonView _playerInputView;
    [SerializeField] GameObject AllGameObject;
    [SerializeField] private VariableJoystick _joystick;
    [SerializeField] private GameObject _jumpButton;
    [SerializeField] private GameObject _interactButton;
    [SerializeField] private Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        __nicknameText.text = _view.Owner.NickName;
        // __nicknameText.text = "AAAA";
        _currSpeed = _speed;
        if (!_view.IsMine)
        {
            PlayerCameraObject.SetActive(false);
            gameObject.SetActive(false);
            _playerCamera.enabled = false;
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
        if (_view.IsMine)
        {
            _playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            if (Application.platform == RuntimePlatform.Android)
            {
                _playerMovementInput = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
            }
            else
            {
                _playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            }
            MovePlayer();
            if (Input.GetKeyDown(KeyCode.L))
            {
                AllGameObject.transform.position = new Vector3(502f, 43f, 480f);
            }
            _anim.SetFloat("Hinput", _playerMovementInput.z);
            _anim.SetFloat("Vinput", _playerMovementInput.x);

        }
    }
    void testdebug()
    {
        Debug.Log("TstDebug");
    }
    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(_playerMovementInput);
        if (_controller.isGrounded)
        {
            _anim.SetBool("Jumping", false);
            if (_playerMovementInput.magnitude > 0.1)
            {
                _anim.SetBool("Walking", true);
            }
            else
            {
                _anim.SetBool("Walking", false);
            }
            _velocity.y = -1f;
            if (Input.GetKeyDown(KeyCode.Space) || _jumpButtonPressed)
            {
                Jump();
            }
        }
        else
        {
            _velocity.y -= _gravity * -2f * Time.deltaTime;
        }
        _controller.Move(_currSpeed * Time.deltaTime * MoveVector);
        _controller.Move(_velocity * Time.deltaTime);
    }
    public void Jump()
    {
        _velocity.y = _jumpForce;
        Debug.Log("Plyaer Jump!");
        _anim.SetBool("Jumping", true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovePlatform"))
        {
            _playerInputView.RPC("AttachChild", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
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
                _anim.SetBool("Pushing", true);
            }
            else
            {
                RemoveJointWithObject(other);
                _anim.SetBool("Pushing", false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovePlatform"))
        {
            _playerInputView.RPC("DetachChild", RpcTarget.All);
        }
        if (other.CompareTag("Grabable"))
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                _interactButton.SetActive(false);
            }
            // other.attachedRigidbody.isKinematic = true;
            _playerInputView.RPC("JoinDisconnect", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
            _anim.SetBool("Pushing", false);
        }
        _currSpeed = _speed;
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
        _playerInputView.RPC("JointConnect", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
        _currSpeed = _grabSpeed;
    }
    private void RemoveJointWithObject(Collider other)
    {
        _playerInputView.RPC("JoinDisconnect", RpcTarget.All, other.gameObject.GetPhotonView().ViewID);
        // other.attachedRigidbody.isKinematic = true;
        _currSpeed = _speed;
    }
    [PunRPC]
    void JointConnect(int targetView)
    {
        PhotonView.Find(targetView).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        _grabJoint.connectedBody = PhotonView.Find(targetView).gameObject.GetComponent<Rigidbody>();

    }

    [PunRPC]
    void JoinDisconnect(int targetView)
    {
        PhotonView.Find(targetView).gameObject.GetComponent<Rigidbody>().isKinematic = true;

        _grabJoint.connectedBody = null;
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
