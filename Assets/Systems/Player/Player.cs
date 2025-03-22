
using Photon.Pun;
using Systems.Mechanisms;
using UnityEngine;

namespace Systems.Player
{
    public class Player : MonoBehaviourPun
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            GameObject interactive = other.gameObject;

            if (interactive.CompareTag("Interactable"))
            {
                Button button = interactive.GetComponent<Button>();
                
                button.Interact();
            }else if (interactive.CompareTag($"ColorChanger"))
            {
                ColorManager colorManager = interactive.GetComponent<ColorManager>();
                
                colorManager.ActivateColorChange();
            }
        }
    }
}
