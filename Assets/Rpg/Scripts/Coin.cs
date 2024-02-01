using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public interface IICollectable
    {
        void Collect(ICharacter character);
    }

    public abstract class CollectableBehaviour : MonoBehaviour, IICollectable
    {

        [SerializeField] protected float dismissTimer;
        [SerializeField] protected ParticleSystem particles;

        public abstract void Collect(ICharacter character);

        private void Awake()
        {
            if (particles) particles.Stop();
        }

        protected void Collect()
        {
            if (!enabled) return;
            enabled = false;
            if (particles) particles.Play();
            StartCoroutine(Dismiss());
        }

        private IEnumerator Dismiss()
        {
            yield return new WaitForSeconds(dismissTimer);
            gameObject.SetActive(false);
        }
    }

    public class Coin : CollectableBehaviour
    {
        [SerializeField] private float value;

        public override void Collect(ICharacter character)
        {
            if (!enabled) return;
            character.AddMoney(value);
            base.Collect();
        }
    }
}
