using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class FireLauncher : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private ActivationArea activationArea;
        [SerializeField] private Vector2 shootDirection;
        [SerializeField] private float shootForce;
        [SerializeField] private float shootingInterval;
        private float shootingTimer;
        private bool isEnabled;

        // Start is called before the first frame update
        void Start()
        {
            activationArea.OnActivate.AddListener(Activate);
            activationArea.OnDeactivate.AddListener(Deactivate);
        }

        // Update is called once per frame
        void Update()
        {
            if (!isEnabled) return;
            shootingTimer += Time.deltaTime;
            if (shootingTimer > shootingInterval)
            {
                shootingTimer = 0;
                Shoot();
            }
            else if (shootingTimer > shootingInterval / 2)
            {
                animator.SetBool("On", false);
            }
        }

        private void Shoot()
        {
            animator.SetBool("On", true);
            var bullet = bulletPool.GetBullet(transform.position);
            bullet.Setup(shootForce, gameObject.tag);
            bullet.Fire(shootDirection);
        }

        private void Activate()
        {
            shootingTimer = 0;
            isEnabled = true;
        }

        private void Deactivate()
        {
            animator.SetBool("On", false);
            isEnabled = false;
        }
    }
}
