using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public interface IInventoryItem
    {
        int GetID();
        void Use(ICharacter character);
    }

    public abstract class InventoryItemBehaviour : MonoBehaviour, IInventoryItem
    {
        [SerializeField] private int id;
        public int GetID()
        {
            return id;
        }
        public abstract void Use(ICharacter character);
    }

    public class HealthItem : InventoryItemBehaviour
    {
        [SerializeField] private float value;

        public override void Use(ICharacter character)
        {
            character.HealHealth(value);
        }
    }
}
