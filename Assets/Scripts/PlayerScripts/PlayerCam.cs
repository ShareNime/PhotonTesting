using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    private float _mouseSense = 1f;
    private float _xAxis, _yAxis;
    [SerializeField] Transform _camFollowPos;
    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidCameraMovement();
        }
        else
        {
            _xAxis += Input.GetAxisRaw("Mouse X") * _mouseSense;
            _yAxis += Input.GetAxisRaw("Mouse Y") * _mouseSense;
            _yAxis = Mathf.Clamp(_yAxis, -60, 60);
        }


    }
    private void LateUpdate()
    {
        _camFollowPos.localEulerAngles = new Vector3(_yAxis, _camFollowPos.localEulerAngles.y, _camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _xAxis, transform.eulerAngles.z);
    }
    void AndroidCameraMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.position.x > Screen.width / 2 && touch.position.y > Screen.height / 4)
            {
                if (touch.phase == TouchPhase.Moved)
                {

                    _xAxis += touch.deltaPosition.x * 0.1f;
                    _yAxis -= touch.deltaPosition.y * 0.1f;

                    _yAxis = Mathf.Clamp(_yAxis, -60, 60);

                }
            }

        }
    }
}
