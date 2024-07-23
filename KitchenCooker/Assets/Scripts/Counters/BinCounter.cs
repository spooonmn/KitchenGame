using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            player.ClearKitchenObject();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
