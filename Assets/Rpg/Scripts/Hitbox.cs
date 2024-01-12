using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace ProjectGo2D.Rpg
{
    public class Hitbox : MonoBehaviour
    {
        [SerializeField, TagField] private string hitTag;
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private DamageType damageType;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(hitTag)) return;
            var enemy = other.GetComponent<ICharacter>();
            if (enemy == null) return;
            character.ApplyDamage(enemy, damageType);
        }
    }
}
