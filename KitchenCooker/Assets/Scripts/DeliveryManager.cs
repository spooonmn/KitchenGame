using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSucess;
    public static DeliveryManager Instance { get; private set; }

    private List<RecipeSO> waitingRecipeList;



    [SerializeField]private RecipeListSO recipeListSO;

    private int deliveredRecipes;


    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeListMax = 4;

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeList.Count < waitingRecipeListMax)
            {
                SpawnRecipe();
            } 
        }
    }

    private void Awake()
    {
        waitingRecipeList = new List<RecipeSO>();
        Instance = this;
        spawnRecipeTimer = spawnRecipeTimerMax;
    }
    

    private void SpawnRecipe()
    {

       RecipeSO waitingRecipeSO =  recipeListSO.recipeSOList[UnityEngine.Random.Range(0,recipeListSO.recipeSOList.Count)];
        waitingRecipeList.Add(waitingRecipeSO);
        
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeList[i];
            if (waitingRecipeSO.kitchenObjectList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {//has same amount of ingridients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectList)
                {//cycling thru all ingridients in the recipe
                    bool ingridientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {//cycling thru all ingridients in the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingridientFound = true;
                            break;
                        }
                    }
                    if (!ingridientFound)
                    {// this recipe ingridient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if(plateContentsMatchesRecipe)
                {// player delivered the correct recipe
                    
                    deliveredRecipes++;
                    waitingRecipeList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSucess?.Invoke(this, EventArgs.Empty);

                    
                    return;
                    
                }
                
            }
        }
        // no matches found
        // player did not deliver a correct recipe
       OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList() 
    {
        return waitingRecipeList;
    }

    public int GetDeliveredRecipes()
    {
        return deliveredRecipes;
    }
}


