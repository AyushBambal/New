using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    public int SceneName = 0;

    private void Awake()
    {
        if (Instance)//check if another RoomManager exists
        {
            Destroy(gameObject);// there can be only 
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

    }

    public override  void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    { 
        if(scene.buildIndex == SceneName)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefab", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }

    }

  

  
}
