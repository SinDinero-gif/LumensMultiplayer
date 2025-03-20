using Photon.Pun;
using UnityEngine;

namespace Systems.Player
{
    
    [RequireComponent(typeof(Rigidbody2D), typeof(PhotonView))]
    public class Movement : MonoBehaviourPun
    {
        private Rigidbody2D _rb;
        
        private AnimatorManager _animatorManager;
        
        public Vector2 movement;

        public float speed;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();

            if (_animatorManager == null)
            {
                _animatorManager = GetComponent<AnimatorManager>();
            }
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
            movement.x = Input.GetAxisRaw("Horizontal");
            
            movement.y = 0;

             movement *= speed;
        }

        private void Move()
        {
            _rb.linearVelocity = new Vector2(movement.x, _rb.linearVelocity.y);
            
        }
    }
}
