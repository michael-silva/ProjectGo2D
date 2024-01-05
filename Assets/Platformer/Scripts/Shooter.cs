using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private Transform shootTarget;
        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private float shootInterval;
        [SerializeField] private float maxChargeTimer;
        [SerializeField] private float chargedForce;
        [SerializeField] private Animator animator;
        private float shootTimer;
        private float chargeTimer;
        private bool isCharging;
        private ICharacter character;

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<ICharacter>();
            particles.Stop();
        }

        // Update is called once per frame
        void Update()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer > shootInterval && Input.GetKeyDown(KeyCode.Keypad1))
            {
                isCharging = true;
                particles.Play();
                chargeTimer = 0;
            }
            if (isCharging)
            {
                chargeTimer += Time.deltaTime;

                if (Input.GetKeyUp(KeyCode.Keypad1))
                {
                    shootTimer = 0;
                    isCharging = false;
                    var main = particles.main;
                    main.startColor = Color.white;
                    particles.Stop();
                    Shoot();
                }
                else if (chargeTimer >= maxChargeTimer)
                {
                    var main = particles.main;
                    main.startColor = Color.red;
                }
                else if (chargeTimer >= maxChargeTimer / 2)
                {
                    var main = particles.main;
                    main.startColor = Color.blue;
                }
            }
        }

        public void Shoot()
        {
            float chargePercent = Math.Min(chargeTimer, maxChargeTimer) / maxChargeTimer;
            float force = 1 + chargePercent * chargedForce;
            animator.SetTrigger("Attack");
            var bullet = bulletPool.GetBullet(shootTarget.position);
            bullet.Setup(force, gameObject.tag);
            bullet.Fire(character.GetFacingDirection());
        }
    }
}
