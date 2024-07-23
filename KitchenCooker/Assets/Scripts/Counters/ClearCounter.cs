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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {//player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetkitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    
                }
                else
                {// player is not carrying a plate but something else

                    if ((GetKitchenObject().TryGetPlate(out  plateKitchenObject)))
                    {// is kitchen counter holding a plate
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetkitchenObjectSO()))
                        {// try add ingridient player is holding to plate
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            // if the player dosent have a kitchen object
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
