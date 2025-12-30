using Runtime.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FridgeCloser: Interaction
{
    public event Action OnClose;

    public override void Interact()
    {
        if (!Interactable) return;
        
        OnClose();
    }
}
