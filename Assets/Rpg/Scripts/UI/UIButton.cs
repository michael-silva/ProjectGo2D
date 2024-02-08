using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectGo2D.Rpg
{
    public class UIButton : MonoBehaviour
    {
        public readonly UnityEvent OnActivate = new UnityEvent();
        public readonly UnityEvent OnFocus = new UnityEvent();
        public readonly UnityEvent OnBlur = new UnityEvent();
        [SerializeField] private Animator animator;
        [SerializeField] private bool focused;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI label;

        // Update is called once per frame
        void Update()
        {
            UpdateAnimation();
        }

        public void Focus()
        {
            OnFocus.Invoke();
            focused = true;
            UpdateAnimation();
        }

        public void Blur()
        {
            OnBlur.Invoke();
            focused = false;
            UpdateAnimation();
        }

        public void Activate()
        {
            OnActivate.Invoke();
        }

        public void EnableImage(Sprite sprite)
        {
            image.sprite = sprite;
            image.enabled = true;
        }
        public void DisableImage()
        {
            image.sprite = null;
            image.enabled = false;
        }
        public void SetLabel(string text)
        {
            label.text = text;
        }

        private void UpdateAnimation()
        {
            animator.SetBool("Focus", focused);
        }
    }
}
