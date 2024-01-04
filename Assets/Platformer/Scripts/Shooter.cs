using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectGo2D.Platformer
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Transform shootTarget;
        [SerializeField] private BulletPool bulletPool;
        [SerializeField] private float shootInterval;
        [SerializeField] private Animator animator;
        private float shootTimer;
        private ICharacter character;

        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<ICharacter>();
        }

        // Update is called once per frame
        void Update()
        {
            shootTimer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Keypad1) && shootTimer > shootInterval)
            {
                shootTimer = 0;
                Shoot();
            }
        }

        public void Shoot()
        {
            animator.SetTrigger("Attack");
            var bullet = bulletPool.GetBullet(shootTarget.position);
            bullet.Fire(character.GetFacingDirection(), gameObject.tag);
        }
    }
}
