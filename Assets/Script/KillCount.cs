using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class KillCount : MonoBehaviour
{
    public TextMeshProUGUI Counter;
    int kills;

    


     void Update()
    {
        ShowKills();
    }

    public void ShowKills()
    {
        Counter.text = kills.ToString();
    }

    public void AddKills()
    {
        kills++;
        Debug.Log("Kills: " + kills);
    }



}
