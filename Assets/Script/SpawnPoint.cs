using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject graphics;
    public bool IsOccupied { get; set; } = false;

    void Start()
    {
        graphics.SetActive(false);
    }
}
