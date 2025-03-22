using Photon.Pun;
using UnityEngine;

namespace Systems.Network
{
    public class PlayerSpawner : MonoBehaviour
    {
        private readonly Vector2 _player1SpawnPos = new Vector2(-12f, 0.5f);
        private readonly Vector2 _player2SpawnPos = new Vector2(12f, 0.5f);
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            Debug.Log("Spawning player");
            
            Vector2 spawnPos = GetSpawnPosition();
            
            PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity);
            
            Debug.Log("Player has Spawned");
        }

        private Vector2 GetSpawnPosition()
        {
            // Get the player's index in the room
            int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;

            // Assign spawn position based on player index
            return playerIndex == 1 ? _player1SpawnPos : _player2SpawnPos;
        }
    }
}
