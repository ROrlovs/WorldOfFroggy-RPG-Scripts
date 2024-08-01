
using UnityEngine;
using Vit.Utilities.Singletons;

public class PlayerInventoryManager : Singleton<PlayerInventoryManager>
{
    public delegate void OnEquipItem();
    public OnEquipItem onEquipItem;
}