using System;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class TeclaCajaFuerte: Interaction
    {
        [SerializeField] private int _numero;
        public event Action<int> OnTeclaPresionada;

        public override void Interact()
        {
            OnTeclaPresionada?.Invoke(_numero);
        }
    }
}