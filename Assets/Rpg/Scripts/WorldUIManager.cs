using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    [Serializable]
    public class ObjectPool<T> where T : MonoBehaviour
    {
        [SerializeField] private int poolLimit;
        [SerializeField] private T prefab;
        private List<T> instances = new List<T>();

        public void Prepare(Transform container)
        {
            for (int i = 0; i < poolLimit; i++)
            {
                var instance = GameObject.Instantiate(prefab, container.position, container.rotation, container);
                instance.gameObject.SetActive(false);
                instances.Add(instance);
            }
        }

        public void Release(T element)
        {
            element.gameObject.SetActive(false);
        }

        public T GetElement(Vector2 position)
        {
            for (int i = 0; i < poolLimit; i++)
            {
                if (instances[i].gameObject.activeSelf) continue;
                instances[i].transform.position = position;
                instances[i].gameObject.SetActive(true);
                return instances[i];
            }
            Debug.LogError("Pool is out of elements");
            return null;
        }
    }

    public class WorldUIManager : MonoBehaviour
    {

        public static WorldUIManager Instance { get; private set; }
        [SerializeField] private Transform interactionBalloon;
        [SerializeField] private ObjectPool<HitLabel> hitsPool;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                Reset();
            }
            else
            {
                Instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            hitsPool.Prepare(transform);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowHit(Vector2 position, float value)
        {
            var hit = hitsPool.GetElement(position);
            hit.OnFinish.AddListener(() => hitsPool.Release(hit));
            hit.Run(value);
        }

        private void Reset()
        {
            interactionBalloon.gameObject.SetActive(false);
        }

        public void ShowInteractive(Vector2 position)
        {
            interactionBalloon.position = position;
            interactionBalloon.gameObject.SetActive(true);
        }

        public void HideInteractive()
        {
            interactionBalloon.gameObject.SetActive(false);
        }
    }
}
