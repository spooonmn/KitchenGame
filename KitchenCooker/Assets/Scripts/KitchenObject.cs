using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;
    



    public KitchenObjectSO GetkitchenObjectSO() 
    {
       return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent KitchenObjectParent)
    {
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = KitchenObjectParent;
        if(KitchenObjectParent.HasKitchenObject())
        {
            Debug.Log("KitchenObjectParent already has a kitchen object");
        }
        KitchenObjectParent.SetKitchenObject(this);

        transform.parent = KitchenObjectParent.GetKitchObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitctchenObjectParent()
    {
        return kitchenObjectParent;
    }


    public Transform GetKitchObjectFollowTransform()
    {
        return transform;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
      

    }
    public KitchenObject GetKitchenObject()
    {
        return this;
    }

    public void ClearKitchenObject()
    {

    }
    
    public bool HasKitchenObject()
    {
        return true;
    }
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }


    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }



}
