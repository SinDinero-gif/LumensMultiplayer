using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Serialization;

namespace Systems.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PhotonView))]
    public class Jump : MonoBehaviourPun
    {
        [HideInInspector]
        public Rigidbody2D rb;

        [Header("Jump Settings")]
        [SerializeField] private float jumpTimer;
        [SerializeField] private int jumpPower;
        [SerializeField] private float fallMultiplier;
        [SerializeField] private float jumpMultiplier;
        [SerializeField] private float jumpDuration;
        public float jumpRaycastDistance;
        
        private int _jumpCount;

        [SerializeField] private LayerMask groundLayer;

        private Vector2 _vecGravity;
        
        [HideInInspector]
        public bool isJumping;
        
        private AnimatorManager _animatorManager;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _vecGravity = new Vector2(0f, -Physics2D.gravity.y);
            rb = GetComponent<Rigidbody2D>();
            _animatorManager = GetComponent<AnimatorManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine) return;
            
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                _animatorManager.photonView.RPC("JumpAnimation", RpcTarget.AllBuffered);
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
                isJumping = true;
                jumpDuration = 0;
            }

            if (rb.linearVelocity.y > 0 && isJumping)
            {
                jumpDuration += Time.deltaTime;
                if(jumpDuration > jumpTimer) isJumping = false;
            }
            
            if (Input.GetButtonUp("Jump") && isJumping)
            {
                isJumping = false;
                
                if(rb.linearVelocityY > 0) rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.3f);
                
            }
            
            
        }

        private void FixedUpdate()
        {
            if (!photonView.IsMine) return;
            
            if (rb.linearVelocityY < 0)
            {
                rb.linearVelocity -= _vecGravity * (fallMultiplier * Time.deltaTime);
            }
        }

        public bool IsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector3.down, jumpRaycastDistance, groundLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded()? Color.green : Color.red;
            Gizmos.DrawRay(transform.position, -transform.up * jumpRaycastDistance);
        }
    }
}
