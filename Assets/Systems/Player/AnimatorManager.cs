using System;
using Photon.Pun;
using UnityEngine;

namespace Systems.Player
{
    public class AnimatorManager : MonoBehaviourPun
    {
        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        private static readonly int IsWalking = Animator.StringToHash("Walking");
        private static readonly int JumpState = Animator.StringToHash("Jump");

        private Jump _jump;
        private Movement _movement;
        private SpriteRenderer _renderer;
        
        private bool _isGrounded;
        
        [SerializeField]
        private Animator animator;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (_jump == null)
            {
                _jump = GetComponent<Jump>();
            }

            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            if (_movement == null)
            {
                _movement = GetComponent<Movement>();
            }

            if (_renderer == null)
            {
                _renderer = GetComponent<SpriteRenderer>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine) return;
            
            if (_jump)
            {
                _isGrounded = _jump.IsGrounded();
            }

            animator.SetBool(IsGrounded, _isGrounded);
            WalkAnimation();
            
            
        }

        private void FixedUpdate()
        {
            FlipSprite();
        }

        [PunRPC]
        public void JumpAnimation()
        {
            animator.SetTrigger(JumpState);
        }

        private void WalkAnimation()
        {
            if (_movement.movement.x != 0 & _isGrounded)
            {
                animator.SetBool(IsWalking, true);
            }
            else
            {
                animator.SetBool(IsWalking, false);
            }
        }
        
        [PunRPC]
        private void FlipSpriteRPC(float faceDir)
        {
            if (faceDir > 0)
            {
                _renderer.flipX = false;
            }
            else if (faceDir < 0)
            {
                _renderer.flipX = true;
            }
        }
        
        private void FlipSprite()
        {
            if (!photonView.IsMine) return; // Only the local player controls flipping

            float faceDirLocal = _movement.movement.x; // Determine direction

            if (faceDirLocal > 0)
            {
                _renderer.flipX = false;
            }
            else if (faceDirLocal < 0)
            {
                _renderer.flipX = true;
            }
            
            photonView.RPC("FlipSpriteRPC", RpcTarget.Others, faceDirLocal); // Sync to others
            
        }
    }
}
