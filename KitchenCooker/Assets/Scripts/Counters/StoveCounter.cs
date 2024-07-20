using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress
{
    public class OnStoveCookingEventArgs : EventArgs
    {
        public bool ToggleOn;
    }
    public event EventHandler <OnStoveCookingEventArgs> OnStoveCooking;

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnKitchenObjectRemoved;

    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }



    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArrray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArrray;

    private State state;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;


    private void Start()
    {
        state = State.Idle;
    }


    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    Frying();
                    break;
                case State.Fried:
                    Fried();
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    private void Frying()
    {
        fryingTimer += Time.deltaTime;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer / fryingRecipeSO.fryingProgressMax });

        if (fryingTimer > fryingRecipeSO.fryingProgressMax)
        {
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

            burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetkitchenObjectSO());
            state = State.Fried;
            burningTimer = 0f;
            

        }
    }

    private void Fried()
    {

        burningTimer += Time.deltaTime;
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = burningTimer / burningRecipeSO.BurningProgressMax });


        if (burningTimer > burningRecipeSO.BurningProgressMax)
        {
            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
            OnKitchenObjectRemoved?.Invoke(this, EventArgs.Empty);
            state = State.Burned;
            
        }
    }

    public override void Interact(Player player) 
    {
        // if there is no kitchen object on the counter
        if (!HasKitchenObject())
        {
            // if the player has a kitchen object
            if (player.HasKitchenObject()) 
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetkitchenObjectSO())) // player has object that can be fried
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetkitchenObjectSO());

                    OnStoveCooking?.Invoke(this, new OnStoveCookingEventArgs { ToggleOn = true });
                    state = State.Frying;
                    fryingTimer = 0f;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer/fryingRecipeSO.fryingProgressMax  });
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
                OnStoveCooking?.Invoke(this, new OnStoveCookingEventArgs { ToggleOn = false });
                state = State.Idle;
                OnKitchenObjectRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
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
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArrray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        //Debug.LogError("No CuttingRecipeSO found for input: " + inputKitchenObjectSO.objectName);
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArrray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        //Debug.LogError("No CuttingRecipeSO found for input: " + inputKitchenObjectSO.objectName);
        return null;
    }
}
