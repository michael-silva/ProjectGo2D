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
        private bool isReady;

        public void Prepare(Transform container = null)
        {
            if (poolLimit <= 0 || prefab == null) return;
            for (int i = 0; i < poolLimit; i++)
            {
                var instance = container == null
                    ? GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity)
                    : GameObject.Instantiate(prefab, container.position, container.rotation, container);
                instance.gameObject.SetActive(false);
                instances.Add(instance);
            }
            isReady = true;
        }

        public void Release(T element)
        {
            element.gameObject.SetActive(false);
        }

        public T? GetElement(Vector2 position)
        {
            if (!IsReady()) return null;
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

        public bool IsReady()
        {
            return isReady;
        }
    }

    public class UIDialog
    {
        private bool isSpeakFinished = false;
        private int current = 0;
        private List<string> speaks;

        public UIDialog(List<string> speaks)
        {
            this.speaks = speaks;
        }


        internal string GetCurrentSpeak()
        {
            return speaks[current];
        }

        internal void SpeakFinished()
        {
            isSpeakFinished = true;
        }

        internal bool IsSpeakFinished()
        {
            return isSpeakFinished;
        }

        internal bool TryGoToNextSpeak()
        {
            if (current == speaks.Count - 1) return false;
            current++;
            isSpeakFinished = false;
            return true;
        }
    }

    public class WorldUIManager : MonoBehaviour
    {

        public static WorldUIManager Instance { get; private set; }
        [SerializeField] private Transform interactionBalloon;
        [SerializeField] private ObjectPool<HitLabel> hitsPool;
        [SerializeField] private float dialogWordInterval;
        [SerializeField] private Transform dialogTransform;
        [SerializeField] private Transform dialogNextIndicator;
        [SerializeField] private TextMeshProUGUI dialogText;
        private Coroutine dialogCoroutine;
        private UIDialog dialog;

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
            if (dialogTransform.gameObject.activeSelf) return;
            GameManager.Instance.PauseGame();
            dialogTransform.position = position;
            dialogTransform.gameObject.SetActive(true);
            dialog = new UIDialog(speaks);
            dialogCoroutine = StartCoroutine(DisplayText());
            InputManager.Instance.OnActionReleased.AddListener(ActionTextDialog);
        }

        public IEnumerator DisplayText()
        {
            string speak = dialog.GetCurrentSpeak();
            dialogText.text = "";
            dialogNextIndicator.gameObject.SetActive(false);
            for (int i = 0; i < speak.Length; i++)
            {
                dialogText.text += speak[i];
                yield return new WaitForSecondsRealtime(dialogWordInterval);
            }
            dialog.SpeakFinished();
            dialogNextIndicator.gameObject.SetActive(true);
        }

        private void ActionTextDialog()
        {
            if (dialog.IsSpeakFinished())
            {
                if (dialog.TryGoToNextSpeak())
                {
                    dialogCoroutine = StartCoroutine(DisplayText());
                }
                else
                {
                    GameManager.Instance.ResumeGame();
                    dialogTransform.gameObject.SetActive(false);
                    InputManager.Instance.OnActionReleased.RemoveListener(ActionTextDialog);
                }
            }
            else
            {
                StopCoroutine(dialogCoroutine);
                dialogText.text = dialog.GetCurrentSpeak();
                dialog.SpeakFinished();
                dialogNextIndicator.gameObject.SetActive(true);
            }
        }
    }
}
