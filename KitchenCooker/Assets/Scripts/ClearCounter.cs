using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        // if there is no kitchen object on the counter
        if (!HasKitchenObject())
        {
            // if the player has a kitchen object
            if (player.GetKitchenObject() != null)
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else { }// player dosent have a kitchen object
        }
        else // there is a kitchen object on the counter
        {
            // if the player has a kitchen object
            if (player.GetKitchenObject())
            {
                
            }
            // if the player dosent have a kitchen object
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
