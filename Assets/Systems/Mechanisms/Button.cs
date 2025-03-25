using System.Collections.Generic;
using Photon.Pun;
using Systems.Mechanisms.Base;
using UnityEngine;

namespace Systems.Mechanisms
{
    public class Button : Interaction
    {
        [SerializeField] private List<Activation> activableObjects;
        
        [PunRPC]
        protected override void OnInteraction()
        {
            foreach (Activation activableObject in activableObjects)
            {
                if (activableObject && colorType == activableObject.colorType)
                {
                    activableObject.Activate();    
                }
                else
                {
                    //Debug.Log($"{activableObject.name} doesn't have a valid color type");
                }
            }   
            
            //Debug.Log("Button is clicked");
        }
    }
}
