using Photon.Pun;
using UnityEngine;

namespace Systems.Mechanisms
{
    public class LevelEndTrigger : MonoBehaviourPun
    {
        [SerializeField] private int triggerID; // Assign a unique ID to each trigger (0 or 1)
        private static bool[] triggersActive = new bool[2]; // Tracks if each trigger is occupied

        private void OnTriggerEnter2D(Collider2D other)
        {
            PhotonView player = other.GetComponent<PhotonView>();
            
            if (other.CompareTag("Player") && PhotonNetwork.IsConnected)
            {
                if (player.IsMine) // Ensure only the local player's instance updates the state
                {
                    photonView.RPC(nameof(SetTriggerState), RpcTarget.AllBuffered, triggerID, true);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            PhotonView player = other.GetComponent<PhotonView>();
            
            if (other.CompareTag("Player") && PhotonNetwork.IsConnected)
            {
                if (player.IsMine)
                {
                    photonView.RPC(nameof(SetTriggerState), RpcTarget.AllBuffered, triggerID, false);
                }
            }
        }

        [PunRPC]
        private void SetTriggerState(int id, bool state)
        {
            triggersActive[id] = state;
            Debug.Log($"Trigger {id} is now {(state ? "ACTIVE" : "INACTIVE")}");

            if (triggersActive[0] && triggersActive[1]) // Both players must be in place
            {
                EndLevel();
            }
        }

        private void EndLevel()
        {
            Debug.Log("Both players are in position! Level Complete!");
            
            PhotonNetwork.LeaveRoom();
        }
    }
}