using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler <OnProgressChangedEventArgs>OnProgressChanged;
    public event EventHandler OnKitchenObjectRemoved;
    public event EventHandler OnCut;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress = 0;

    public override void Interact(Player player) 
    {
        // if there is no kitchen object on the counter
        if (!HasKitchenObject())
        {
            // if the player has a kitchen object
            if (player.GetKitchenObject() != null)
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetkitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput((GetKitchenObject().GetkitchenObjectSO()));
                    
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / (float)cuttingRecipeSO.cuttingProgressMax });
                }
                
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
                OnKitchenObjectRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetkitchenObjectSO()))
        {
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput((GetKitchenObject().GetkitchenObjectSO()));
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs { progressNormalized = (float)cuttingProgress / (float)cuttingRecipeSO.cuttingProgressMax });
            OnCut?.Invoke(this, EventArgs.Empty);

            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                //there is a kitchen object here
                KitchenObjectSO outputKitchenObject = GetOutputForInput(GetKitchenObject().GetkitchenObjectSO());
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
                

            }
        }
    }


    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSO != null) 
        {
           return cuttingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        //Debug.LogError("No CuttingRecipeSO found for input: " + inputKitchenObjectSO.objectName);
        return null;
    }
}
