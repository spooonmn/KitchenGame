using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<IngredientAddedEventArgs> OnIngredientAdded;
    public class IngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }


    private List<KitchenObjectSO> kitchenObjectSOList;
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {// not a valid ingredient
            return false;
        }

        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {//already has ingredient
            return false;
        }
        else
        {//new ingredient
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new IngredientAddedEventArgs { kitchenObjectSO = kitchenObjectSO });
            return true;  
        }

    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
