using System;
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

    [Serializable]
    public struct SpawnItem
    {
        public CollectableBehaviour collectable;
        public float chance;
        public float quantity;

        public static void Spawn(List<SpawnItem> items, Transform transform)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                for (int j = 0; j < item.quantity; j++)
                {
                    if (item.chance < UnityEngine.Random.Range(0f, 1f)) continue;
                    var collectable = GameObject.Instantiate(item.collectable, transform.position, transform.rotation, transform);
                    collectable.Push(new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)), 1f);
                }
            }
        }
    }

    public class Chest : MonoBehaviour, IInteractive
    {
        [SerializeField] private Animator animator;
        [SerializeField] private List<SpawnItem> items;
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
            SpawnItem.Spawn(items, transform);
            Disable();
            enabled = false;
        }

    }
}
