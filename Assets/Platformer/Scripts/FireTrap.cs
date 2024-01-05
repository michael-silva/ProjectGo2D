using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class FireTrap : MonoBehaviour
    {
        private Damager damager;
        [SerializeField] private ActivationArea activationArea;
        [SerializeField] private int blinkNumbers;
        [SerializeField] private float damageDelay;
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            damager = GetComponent<Damager>();
            activationArea.OnActivate.AddListener(Activate);
            activationArea.OnDeactivate.AddListener(Deactivate);
        }


        private IEnumerator TurningOn()
        {
            float interval = damageDelay / (blinkNumbers * 2);
            for (int i = 0; i < blinkNumbers; i++)
            {
                sprite.color = new Color(1, 0, 0, 1);
                yield return new WaitForSeconds(interval);
                sprite.color = Color.white;
                yield return new WaitForSeconds(interval);
            }
            animator.SetBool("On", true);
            damager.enabled = true;
        }

        private void Activate()
        {
            StartCoroutine(TurningOn());
        }

        private void Deactivate()
        {
            StopCoroutine(TurningOn());
            animator.SetBool("On", false);
            damager.enabled = false;
        }
    }
}
