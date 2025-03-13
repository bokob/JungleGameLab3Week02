using System;
using UnityEngine;


public class GameManager
{



    public Action<GameObject> OnPlayerSpawn;
   
    public void SpawnPlayer(GameObject _playerPrefabs)
    {
        OnPlayerSpawn?.Invoke(_playerPrefabs);
    }
}   