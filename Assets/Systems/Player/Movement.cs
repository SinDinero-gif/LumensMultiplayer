using Photon.Pun;
using Systems.Mechanisms;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems.Player
{
    
    [RequireComponent(typeof(Rigidbody2D), typeof(PhotonView))]
    public class Movement : MonoBehaviourPun
    {
        private Rigidbody2D _rb;
        
        private Vector2 _movement;

        public float speed;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine) return;
            
            MoveDirection();
            Move();
            
        }
        
        private void MoveDirection()
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
            
            _movement.y = 0;

             _movement *= speed;
        }

        private void Move()
        {
            _rb.linearVelocity = new Vector2(_movement.x, _rb.linearVelocity.y);
        }
    }
}
