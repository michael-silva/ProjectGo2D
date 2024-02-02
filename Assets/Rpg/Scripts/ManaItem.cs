using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class ManaItem : InventoryItemBehaviour
    {
        [SerializeField] private float value;

        public override void Use(ICharacter character)
        {
            character.FillMana(value);
        }

    }
}
