using Photon.Pun;
using UnityEngine;

namespace Systems.Mechanisms.Base
{
    public abstract class Activation : ColorData
    {
        [PunRPC]
        protected abstract void OnActivation();
        
        public void Activate()
        {
            photonView.RPC(nameof(OnActivation), RpcTarget.AllBuffered);
        }
    }
}
