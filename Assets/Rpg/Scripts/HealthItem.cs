using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectGo2D.Rpg
{
    public interface IInventoryItem
    {
        int GetID();
        void Use(ICharacter character);
        string GetName();
        string GetDescription();
        Sprite GetImage();
    }

    public abstract class InventoryItemBehaviour : MonoBehaviour, IInventoryItem
    {
        [SerializeField] private Sprite image;
        [SerializeField] private string nameLabel;
        [SerializeField] private string description;
        [SerializeField] private int id;
        public int GetID()
        {
            return id;
        }
        public abstract void Use(ICharacter character);


        public string GetName() { return nameLabel; }
        public string GetDescription() { return description; }
        public Sprite GetImage() { return image; }
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
