using Runtime.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeCloser: Interaction
{
    public override void Interact()
    {
        if (!Interactable) return;
        Debug.Log("JEJE");
    }
}
