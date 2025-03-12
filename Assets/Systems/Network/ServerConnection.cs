using System;
using Photon.Pun;
using UnityEngine;

namespace Systems.Network
{
    public class ServerConnection : MonoBehaviourPunCallbacks
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        
        public override void OnConnected()
        {
            base.OnConnected();
            
            print("is Connected!");
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            
            print("is Connected To Master!");
            
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            
            print ("Has Joined To Lobby!");
        }
    }
}
