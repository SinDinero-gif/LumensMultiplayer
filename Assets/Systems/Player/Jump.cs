using System;
using Photon.Pun;
using UnityEngine;

namespace Systems.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PhotonView))]
    public class Jump : MonoBehaviourPun
    {
        private Rigidbody2D _rb;

        [Header("Jump Settings")]
        [SerializeField] private float jumpTimer;
        [SerializeField] private int jumpPower;
        [SerializeField] private float fallMultiplier;
        [SerializeField] private float jumpMultiplier;
        [SerializeField] private float jumpDuration;
        
        
        private int _jumpCount;

        [SerializeField] private LayerMask groundLayer;

        private Vector2 _vecGravity;
        
        private bool _isJumping;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _vecGravity = new Vector2(0f, -Physics2D.gravity.y);
            _rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine) return;
            
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                Debug.Log("Jump");
                _rb.linearVelocity = new Vector2(_rb.linearVelocityX, jumpPower);
                _isJumping = true;
                jumpDuration = 0;
            }

            if (_rb.linearVelocity.y > 0 && _isJumping)
            {
                jumpDuration += Time.deltaTime;
                if(jumpDuration > jumpTimer) _isJumping = false;
            }
            
            if (Input.GetButtonUp("Jump") && _isJumping)
            {
                _isJumping = false;
                
                if(_rb.linearVelocityY > 0) _rb.linearVelocity = new Vector2(_rb.linearVelocityX, _rb.linearVelocityY * 0.3f);
                
            }
            
            
        }

        private void FixedUpdate()
        {
            if (!photonView.IsMine) return;
            
            if (_rb.linearVelocityY < 0)
            {
                _rb.linearVelocity -= _vecGravity * (fallMultiplier * Time.deltaTime);
            }
        }

        private bool IsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded()? Color.green : Color.red;
            Gizmos.DrawRay(transform.position, -transform.up * 1.1f);
        }
    }
}
