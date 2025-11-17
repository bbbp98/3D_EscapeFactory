using UnityEngine;

public enum ItemType
{
    Score,
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Infomation")]
    public ItemType type;
    public string itemName;
    public int value;
}
