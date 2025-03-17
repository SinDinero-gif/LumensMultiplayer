using DG.Tweening;
using Photon.Pun;
using Systems.Mechanisms.Base;
using UnityEngine;

namespace Systems.Mechanisms
{
    public class Door : Activation
    {
        public Transform endPointTransform;
        public float duration = 2f;
        
        private Vector2 _endPoint;

        protected override void Start()
        {
            base.Start();
            
            _endPoint = endPointTransform.position;
        }

        
        
        [PunRPC]
        protected override void OnActivation()
        {
            transform.DOMove(_endPoint, duration).SetEase(Ease.InOutSine);
            Debug.Log("Door activated");
        }

        
    }
}
