using UnityEngine;

// ScriptableObject representing a kitchen object
[CreateAssetMenu(fileName = "KitchenObject", menuName = "ScriptableObjects/KitchenObjectSO", order = 1)]
public class KitchenObjectSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite objectSprite;
    public string objectName;


}
