using Photon.Pun;
using Systems.Mechanisms.Base;
using UnityEngine;

namespace Systems.Mechanisms
{
    public class Door : Activation
    {
        [PunRPC]
        protected override void OnActivation()
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + 5f), 8f);
            Debug.Log("Door activated");
        }

        
    }
}
