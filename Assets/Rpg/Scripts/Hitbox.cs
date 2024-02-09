using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public class Hitbox : MonoBehaviour
    {
        public readonly UnityEvent OnHit = new UnityEvent();
        [SerializeField] private ParticleSystem hitEffect;
        [SerializeField, TagField] private string hitTag;
        [SerializeField] private CharacterBehaviour character;
        [SerializeField] private DamageType damageType;
        [SerializeField] private float impact;

        // Start is called before the first frame update
        void Start()
        {
            hitEffect.Stop();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!enabled && !other.CompareTag(hitTag)) return;
            var enemy = other.GetComponent<ICharacter>();
            if (enemy == null) return;
            if (character.ApplyDamage(enemy as CharacterBehaviour, damageType))
            {
                hitEffect.transform.position = other.transform.position;
                hitEffect.Play();
                var dir = other.transform.position - transform.position;
                dir.Normalize();
                enemy.ApplyImpact(dir, impact);
            }
            OnHit.Invoke();
        }

        private void OnEnable()
        {
            hitEffect.Stop();
        }
    }
}
