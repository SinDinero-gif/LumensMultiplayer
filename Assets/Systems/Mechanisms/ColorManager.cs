using Photon.Pun;
using Systems.Mechanisms.Base;
using UnityEngine;

namespace Systems.Mechanisms
{
    [RequireComponent(typeof(PhotonView))]
    public class ColorManager : ColorData
    {
        [SerializeField] private ColorData colorObject;

        protected override void Start()
        {
            base.Start();
            colorObject = colorObject.GetComponent<ColorData>();
        }

        [PunRPC]
        private void ColorChange()
        {
            if (colorObject!= null)
            {
                colorObject.colorType = colorType;
                colorObject.ColorSet();
            }
            else
            {
                colorObject = colorObject.GetComponent<ColorData>();
            }
        }

        public void ActivateColorChange()
        {
            photonView.RPC(nameof(ColorChange), RpcTarget.AllBuffered);
        }
    }
}
