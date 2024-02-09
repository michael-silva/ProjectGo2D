using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProjectGo2D.Shared;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    [System.Serializable]
    public class InventorySlot
    {
        public IInventoryItem item;
        public int quantity;

        public InventorySlot(IInventoryItem item)
        {
            this.item = item;
            this.quantity = 1;
        }
    }

    [System.Serializable]
    public class Inventory
    {
        [SerializeField] private int maxSlots;
        [SerializeField, ReadOnly] private List<InventorySlot> slots;

        public List<InventorySlot> GetItems()
        {
            return slots;
        }

        public InventorySlot? GetItem(int index)
        {
            return HasElement(index) ? slots.ElementAt(index) : null;
        }

        public bool HasElement(int index)
        {
            return index >= 0 && index < slots.Count;
        }

        public int IndexOf(IInventoryItem item)
        {
            return slots.FindIndex(slot => slot.item.GetID() == item.GetID());
        }

        public bool TryAddItem(IInventoryItem item)
        {
            int index = IndexOf(item);
            if (index == -1)
            {
                if (slots.Count >= maxSlots) return false;
                slots.Add(new InventorySlot(item));
                return true;
            }
            else
            {
                var slot = slots[index];
                slot.quantity++;
                return true;
            }
        }


        public bool TryRemoveItem(int index)
        {
            if (!HasElement(index)) return false;
            var slot = slots[index];
            slot.quantity--;
            if (slot.quantity == 0)
            {
                slots.RemoveAt(index);
            }
            return true;
        }
    }

    public enum DamageType
    {
        Melee,
        Shoot,
        Magic,
    }

    public interface ICharacter
    {
        float GetSpeed();
        void HealHealth(float heal);
        void FillMana(float value);
        void GainXP(float value);
        float GetXP();
        bool IsDead();
        bool TakeDamage(float damage, DamageType type);
        bool ApplyDamage(ICharacter enemy, DamageType type);
        void ApplyImpact(Vector2 direction, float impact);
        void AddMoney(float value);
        bool TryAddToInventory(IInventoryItem item);
        bool TryRemoveFromInventory(int index);
        List<InventorySlot> GetInventoryItems();
        InventorySlot? GetInventoryItemAt(int index);

    }

}
