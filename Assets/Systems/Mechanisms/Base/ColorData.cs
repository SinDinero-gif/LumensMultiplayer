using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using static UnityEngine.Color;

namespace Systems.Mechanisms.Base
{
    [RequireComponent(typeof(PhotonView))]
    public abstract class ColorData : MonoBehaviourPun
    {
        public EnuColors colorType;
        
        protected static Dictionary<Enum, Color> colorMap = new Dictionary<Enum, Color>()
        {
            {EnuColors.Red, red},
            {EnuColors.White, white},
            {EnuColors.Blue, blue},
            {EnuColors.Green, green},
            {EnuColors.Yellow, yellow},
            {EnuColors.Orange, new Color(1f, 0.5f, 0)},
            {EnuColors.Purple, new Color(0.5f, 0, 0.5f)}
        };
        
        protected SpriteRenderer spriteRenderer;

        protected virtual void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void Start()
        {
            photonView.RPC(nameof(ColorSet), RpcTarget.AllBuffered);
        }

        [PunRPC]
        public virtual void ColorSet()
        {
            if (colorMap.TryGetValue(colorType, out Color selectedColor))
            {
                spriteRenderer.color = selectedColor;
            }
            else
            {
                Debug.LogWarning($"Color {colorType} not found in {gameObject.name}");
            }
        }
        
    }

    public enum EnuColors
    {
        Red,
        White,
        Blue,
        Green,
        Yellow,
        Orange,
        Purple,
    }
}
