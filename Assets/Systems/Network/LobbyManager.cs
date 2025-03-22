using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Systems.Network
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private int maxPlayers = 2;
        [SerializeField] private TextMeshProUGUI playerCountText;

        void Start()
        {
            UpdatePlayerCount();
        }

        public override void OnJoinedRoom()
    {
        UpdatePlayerCount();
        CheckPlayers();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdatePlayerCount();
        CheckPlayers();
    }
        
        private void UpdatePlayerCount()
        {
            if (playerCountText)
            {
                playerCountText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {maxPlayers}";
            }
        }
        
        private void CheckPlayers()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers && PhotonNetwork.IsMasterClient)
            {
                // Master Client tells all players to load the GameScene
                StartCoroutine(LobbyToGame());
            }
        }

        private IEnumerator LobbyToGame()
        {
            yield return new WaitForSeconds(2f);
            
            photonView.RPC("StartGame", RpcTarget.All);
        }

        [PunRPC]
        private void StartGame()
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
