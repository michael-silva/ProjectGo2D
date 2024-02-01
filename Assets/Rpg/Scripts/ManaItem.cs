using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class ManaItem : CollectableBehaviour
    {
        [SerializeField] private float value;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Collect(ICharacter character)
        {
            if (!enabled) return;
            character.FillMana(value);
            base.Collect();
        }

    }
}
