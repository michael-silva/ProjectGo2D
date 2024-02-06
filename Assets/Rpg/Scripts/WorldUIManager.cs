using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectGo2D.Rpg
{
    [Serializable]
    public class ObjectPool<T> where T : MonoBehaviour
    {
        [SerializeField] private int poolLimit;
        [SerializeField] private T prefab;
        private List<T> instances = new List<T>();

        public void Prepare(Transform container = null)
        {
            for (int i = 0; i < poolLimit; i++)
            {
                var instance = container == null
                    ? GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity)
                    : GameObject.Instantiate(prefab, container.position, container.rotation, container);
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
        [SerializeField] private float dialogWordInterval;
        [SerializeField] private Transform dialog;
        [SerializeField] private Transform dialogNextIndicator;
        [SerializeField] private TextMeshProUGUI dialogText;

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

        public void ShowDialog(Vector2 position, List<string> speaks)
        {
            Time.timeScale = 0;
            dialog.position = position;
            dialog.gameObject.SetActive(true);
            StartCoroutine(DisplayText(speaks));
        }

        public IEnumerator DisplayText(List<string> speaks, int index = 0)
        {
            string speak = speaks[index];
            dialogText.text = "";
            dialogNextIndicator.gameObject.SetActive(false);
            for (int i = 0; i < speak.Length; i++)
            {
                dialogText.text += speak[i];
                yield return new WaitForSecondsRealtime(dialogWordInterval);
            }

            if (index + 1 == speaks.Count)
            {
                Time.timeScale = 1;
                dialog.gameObject.SetActive(false);
            }
            else
            {
                DisplayNextText(speaks, index + 1);
            }
        }

        public void DisplayNextText(List<string> speaks, int index)
        {
            dialogNextIndicator.gameObject.SetActive(true);
            UnityAction runNextText = () => StartCoroutine(DisplayText(speaks, index));
            UnityAction removeListener = () => InputManager.Instance.OnActionCalled.RemoveListener(runNextText);
            InputManager.Instance.OnActionCalled.AddListener(runNextText);
        }
    }
}
