using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    GameObject controller;
    GameObject controller2;
    int Kills;
    int respawnsLeft = 3; // Set the maximum number of respawns here
    

   Scoreboard scoreboard;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
     

    }

    public void CreateController()
    {
        // Check if a controller already exists
        if (controller != null)
        {
            // Destroy the existing controller
            PhotonNetwork.Destroy(controller);
        }

        if (controller2 != null)
        {
            // Destroy the existing controller
            PhotonNetwork.Destroy(controller2);
        }

       

        if (respawnsLeft > 0)
        {
            if (PhotonNetwork.IsMasterClient)
            {

                


                Transform spawnpoint = SpawnManager.Instance.GetSpawnPoint();
                // Instantiate the host's character
                controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "Player1"), spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
            }
            else
            {

             

                Transform spawnpoint1 = SpawnManager.Instance.GetSpawnPoint();
                // Instantiate the joined player's character
                controller2 = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "Player2"), spawnpoint1.position, spawnpoint1.rotation, 0, new object[] { PV.ViewID });
            }

            Debug.Log("Maja aagya");
        }
        else
        {
            // No more respawns left, handle this as needed (e.g., show game over screen, etc.)
            Debug.Log("No more respawns left.");
        }
    }

    public void Die()
    {
        if (respawnsLeft > 0)
        {
            respawnsLeft--;

            if (controller != null)
            {
                PhotonNetwork.Destroy(controller);
                CreateController();
            }

            if (controller2 != null)
            {
                PhotonNetwork.Destroy(controller2);
                CreateController();
            }
        }
        else
        {
            // No more respawns left, handle this as needed (e.g., show game over screen, etc.)
            Debug.Log("No more respawns left.");
        }

        

    }

    public void GetKill()
    {
        PV.RPC(nameof(Rpc_GetKill), PV.Owner);
       
    }

    [PunRPC]
    void Rpc_GetKill()
    {
        Kills++;

       

        // Update the scoreboard


        Debug.Log("SS");
        // Destroy the current wall


        Hashtable hash = new Hashtable();
        hash.Add("kills", Kills);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    


    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
    }
}
