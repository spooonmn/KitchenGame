using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
public class RecipeSO : ScriptableObject
{

    public List<KitchenObjectSO> kitchenObjectList;

    public string recipeName;


}
