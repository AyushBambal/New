using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Scoreboard : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform container;
    [SerializeField] GameObject scoreboarditemPrefab;
    [SerializeField] CanvasGroup canvasGroup;
    public GameObject Vs;

    Dictionary<Player, ScoreboardItem> scoreboardItem = new Dictionary<Player, ScoreboardItem>();
  

    private PlayerController playerController;
    public GameObject obj;
    


    private void Start()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            AddScoreBoardItem(player);
        }

     

    }

    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddScoreBoardItem(newPlayer);
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemoveScoreboardItem(otherPlayer);
    }

    void AddScoreBoardItem(Player player)
    {
        ScoreboardItem item = Instantiate(scoreboarditemPrefab, container).GetComponent<ScoreboardItem>();
        item.Initialize(player);
        scoreboardItem[player] = item;
    }

    void RemoveScoreboardItem(Player player)
    {
        Destroy(scoreboardItem[player].gameObject);
        scoreboardItem.Remove(player);
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            canvasGroup.alpha = 1;
            Vs.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            canvasGroup.alpha = 0;
            Vs.SetActive(false);
        }
        
    }

    public void Rpc_Sss()
    {
        obj.SetActive(false);//changes to do



    }

}
