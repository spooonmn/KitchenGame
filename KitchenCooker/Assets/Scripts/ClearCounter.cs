using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObject;
    [SerializeField] private Transform counterTopPoint;
    public void Interact()
    {
        Debug.Log("Clear Counter Interacted");
        Transform kitchenObjectTransform = Instantiate(kitchenObject.prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
    }
}
