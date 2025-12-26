using Runtime.Application;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class Interaction: MonoBehaviour, Hoverable
    {
        public Sprite Icon => UIResources.Interact.Icon;
    }
}