using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{

    public class InventoryItem : CollectableBehaviour
    {
        [SerializeField] private InventoryItemBehaviour item;


        public override void Collect(ICharacter character)
        {
            if (!enabled) return;
            if (character.TryAddToInventory(item))
            {
                base.Collect();
            }
        }
    }
}
