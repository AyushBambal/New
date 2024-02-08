using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerManagerName : MonoBehaviour
{
    [SerializeField] TMP_InputField userInput;


     void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            userInput.text = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");
        }
        else
        {
            userInput.text = "Player" + Random.Range(0, 10000).ToString("0000");
            OnUsernameChangeInput();
        }
    }

    public void OnUsernameChangeInput()
    {
        PhotonNetwork.NickName = userInput.text;
        PlayerPrefs.SetString("username", userInput.text);
    }
}
