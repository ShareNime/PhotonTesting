using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class UsernameScript : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject UsernamePanel;
    [SerializeField] TMP_Text MyUsername;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveUsername(){
        PhotonNetwork.NickName = inputField.text;
        // PlayerPrefs.SetString("Username", inputField.text);
        MyUsername.text = inputField.text;
        UsernamePanel.SetActive(false);
    }
}
