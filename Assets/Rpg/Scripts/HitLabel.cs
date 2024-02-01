using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public class HitLabel : MonoBehaviour
    {
        public readonly UnityEvent OnFinish = new UnityEvent();
        [SerializeField] private Animator animator;
        [SerializeField] private float duration;
        [SerializeField] private TextMeshProUGUI text;


        public void Run(float value)
        {
            text.text = Mathf.FloorToInt(value).ToString();
            animator.Play("Hit_Default", -1, 0);
            StartCoroutine(End());
        }

        public IEnumerator End()
        {
            yield return new WaitForSeconds(duration);
            OnFinish.Invoke();
        }
    }
}
