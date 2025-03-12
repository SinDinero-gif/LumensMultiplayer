using Photon.Pun;
using UnityEngine;

namespace Systems.Network
{
    public class PlayerSpawner : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SpawnPlayer();
        }

        private void SpawnPlayer()
        {
            Debug.Log("Spawning player");
            
            Vector2 spawnPos = RandomPoint(0, 1.5f);
            
            PhotonNetwork.Instantiate("Player", spawnPos, Quaternion.identity);
            
            Debug.Log("Player has Spawned");
        }

        private Vector2 RandomPoint(float x, float y)
        {
            x = Random.Range(-8.0f, 8.0f);
            
            return new Vector2(x, y);
        }
    }
}
