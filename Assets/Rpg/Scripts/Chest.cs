using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public interface IInteractive
    {
        void Enable();
        void Interact();
        void Disable();
    }
    public class Chest : MonoBehaviour, IInteractive
    {
        [SerializeField] private Animator animator;
        private bool active;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Disable()
        {
            if (!active || !enabled) return;
            active = false;
            WorldUIManager.Instance.HideInteractive();
        }

        public void Enable()
        {
            if (active || !enabled) return;
            active = true;
            WorldUIManager.Instance.ShowInteractive(transform.position);
        }

        public void Interact()
        {
            if (!active || !enabled) return;
            animator.SetTrigger("Active");
            Disable();
            enabled = false;
        }

    }
}
