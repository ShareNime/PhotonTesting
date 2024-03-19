using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRolling : MonoBehaviour
{
    [SerializeField] float _timeToLive = 15f;
    private void Start() {
        Destroy(gameObject, _timeToLive);
    }
}
