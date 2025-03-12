using Photon.Pun;
using Systems.Mechanisms.Base;
using UnityEngine;

namespace Systems.Mechanisms
{
    public class Button : Interaction
    {
        [SerializeField] Activation activableObject;
        
        [PunRPC]
        protected override void OnInteraction()
        {
            if (colorType == activableObject.colorType)
            {
                activableObject.Activate();    
            }
            else
            {
                Debug.Log($"{activableObject.name} doesn't have a valid color type");
            }
            
            
            Debug.Log("Button is clicked");
        }
    }
}
