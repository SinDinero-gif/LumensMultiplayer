
using Photon.Pun;

namespace Systems.Mechanisms.Base
{
    public abstract class Interaction : ColorData
    {
        [PunRPC]
        protected abstract void OnInteraction();
        
        public void Interact()
        {
            photonView.RPC(nameof(OnInteraction), RpcTarget.AllBuffered);
        }
    }
}
