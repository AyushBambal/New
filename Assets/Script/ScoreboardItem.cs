using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreboardItem : MonoBehaviourPunCallbacks
{
    public TMP_Text usernameText;
    public TMP_Text killsText;
    Player player;

    public void Initialize(Player player)
    {
        usernameText.text = player.NickName;
        this.player = player;

        UpdateStats();
    }

    void UpdateStats()
    {
        if (player.CustomProperties.TryGetValue("kills", out object kills))
        {
            killsText.text = kills.ToString();
        }
        else
        {
            // Handle the case where "kills" property is not present.
            killsText.text = "0";
        }
    }

   public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
{
    if (targetPlayer == player && changedProps.ContainsKey("kills"))
    {
        UpdateStats();
    }
}

}
