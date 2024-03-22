using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    private float mouseSense = 1f;
    float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidCameraMovement();
        }
        else
        {
            xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
            yAxis += Input.GetAxisRaw("Mouse Y") * mouseSense;
            yAxis = Mathf.Clamp(yAxis, -60, 60);
        }


    }
    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
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

                    xAxis += touch.deltaPosition.x * 0.1f;
                    yAxis -= touch.deltaPosition.y * 0.1f;

                    yAxis = Mathf.Clamp(yAxis, -60, 60);

                }
            }

        }
    }
}
