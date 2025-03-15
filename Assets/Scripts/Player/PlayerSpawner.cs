using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    //private void Start()
    //{
    //   Managers.Game.OnPlayerSpawn += SpawnPlayer;
    //}

    private void SpawnPlayer(GameObject _playerPrefabs)
    {
        Instantiate(_playerPrefabs, transform.position, Quaternion.identity);
    }
}
