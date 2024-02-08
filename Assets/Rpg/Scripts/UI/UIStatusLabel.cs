using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ProjectGo2D.Rpg
{
    public class UIStatusLabel : UIButton
    {
        [SerializeField] private TextMeshProUGUI valueLabel;
        [SerializeField] private TextMeshProUGUI nextLabel;
        private bool hasPointsAvailable;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimation();
        }

        protected override void UpdateAnimation()
        {
            base.UpdateAnimation();
            animator.SetBool("Available", hasPointsAvailable);
        }

        public void Setup(float value, bool isAvailable)
        {
            Setup(Mathf.CeilToInt(value), isAvailable);
        }
        public void Setup(int value, bool isAvailable)
        {
            hasPointsAvailable = isAvailable;
            valueLabel.text = value.ToString();
            nextLabel.text = (value + 1).ToString();
        }

        public void Increase(bool isAvailable)
        {
            animator.SetTrigger("Increased");
            StartCoroutine(UpdateIncreasedButton(isAvailable));
        }


        private IEnumerator UpdateIncreasedButton(bool isAvailable)
        {
            yield return new WaitForSeconds(0.5f);
            int value = int.Parse(nextLabel.text);
            Setup(value, isAvailable);
        }
    }
}
