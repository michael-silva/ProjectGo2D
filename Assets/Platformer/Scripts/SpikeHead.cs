using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class SpikeHead : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private ActivationArea activationArea;
        [SerializeField] private Transform moveTarget;
        [SerializeField] private float blinkInterval;
        [SerializeField] private float speed;
        private Vector3 velocity = Vector3.zero;
        private Vector3 defaultPosition;
        private Vector3 currentPosition;
        private float blinkTimer;

        // Start is called before the first frame update
        void Start()
        {
            defaultPosition = transform.position;
            currentPosition = transform.position;
            activationArea.OnActivate.AddListener(Activate);
            activationArea.OnDeactivate.AddListener(Deactivate);
        }

        // Update is called once per frame
        void Update()
        {
            if (Mathf.Abs(Vector3.Distance(transform.position, currentPosition)) > 0.1)
            {
                transform.position = Vector3.Lerp(transform.position, currentPosition, speed * Time.deltaTime);
            }
            blinkTimer += Time.deltaTime;
            if (blinkTimer > blinkInterval)
            {
                blinkTimer = 0;
                animator.SetTrigger("Blink");
            }
        }


        private void Activate()
        {
            currentPosition = moveTarget.position;
        }

        private void Deactivate()
        {
            currentPosition = defaultPosition;
        }
    }
}
