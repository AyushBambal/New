using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class UserNameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView  PlayerPV;
    [SerializeField] TMP_Text text;

    private void Start()
    {
        text.text = PlayerPV.Owner.NickName;
    }

}
