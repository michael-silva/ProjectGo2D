using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Platformer
{

    public class ActivationArea : MonoBehaviour
    {
        [SerializeField] private float activationDelay;
        [SerializeField] private float deactivationDelay;
        public readonly UnityEvent OnActivate = new UnityEvent();
        public readonly UnityEvent OnDeactivate = new UnityEvent();

        private void OnTriggerEnter2D(Collider2D other)
        {
            StartCoroutine(Activate());
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            StartCoroutine(Deactivate());
        }

        private IEnumerator Activate()
        {
            yield return new WaitForSeconds(activationDelay);
            OnActivate.Invoke();

        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(deactivationDelay);
            OnDeactivate.Invoke();
        }
    }
}
