using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Systems.Network
{
    public class Rooms : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private TMP_InputField joinInputfield, createInputField;
        
        [SerializeField] private string roomName;

        private void Start()
        {
            createInputField?.onValueChanged.AddListener(text => { roomName = text; });
            joinInputfield?.onValueChanged.AddListener(text => { roomName = text; });
        }

        [ContextMenu("Create Room")]
        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(roomName);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            
            print($"Room Creation Failed! Code: {returnCode}, Error: {message}");
        }
        
        [ContextMenu("Join Room")]
        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        [ContextMenu("Leave Room")]
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            
            print($"Has Created The Room! Name: {roomName}");
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            print($"Has Joined The Room! Name: {PhotonNetwork.CurrentRoom.Name}");
            
            PhotonNetwork.LoadLevel("Game");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            
            print($"Room Connection Failed!\nCode: {returnCode}, Error: {message}");
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            
            print($"Has Left The Room!");
        }
        
        
    }
}
