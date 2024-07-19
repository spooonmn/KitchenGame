using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        // if there is no kitchen object on the counter
        if (player.HasKitchenObject())
        {
            if (!HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {

                GetKitchenObject().DestroySelf();
                ClearKitchenObject();
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }


        
    }
}
