using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectGo2D.Rpg
{
    public class CharacterBehaviour : MonoBehaviour, ICharacter
    {
        const int MAX_MONEY = 99999;
        public readonly UnityEvent<float, float> OnHealthChange = new UnityEvent<float, float>();
        public readonly UnityEvent<float, float> OnManaChange = new UnityEvent<float, float>();
        public readonly UnityEvent<float, float> OnMoneyChange = new UnityEvent<float, float>();
        public readonly UnityEvent OnInventoryChange = new UnityEvent();


        [SerializeField] private Vector2 direction;
        [SerializeField] private float health;
        [SerializeField] private float mana;
        [SerializeField] private float money;
        [SerializeField] private float exp;
        [SerializeField] private float levelUpXp;
        [SerializeField] private float levelUpIncreaseRate;
        [SerializeField] private int level;
        [SerializeField] private int levelPoints;
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxMana;
        [SerializeField] private float strength;
        [SerializeField] private float defense;
        [SerializeField] private float speed;
        [SerializeField] private float magic;
        [SerializeField] private float shot;
        [SerializeField] private float resistance;
        [SerializeField] private float criticalChance;
        [SerializeField] private float invulnerableDuration;
        [SerializeField] private Inventory inventory;
        private bool isInvulnerable;
        private Vector2 receivedImpact;

        [SerializeField] private LayerMask collisionLayer;
        [SerializeField] private float collisionDistance;
        private BoxCollider2D boxCollider;

        void Start()
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        void Update()
        {
            if (receivedImpact == Vector2.zero) return;
            TryMove(boxCollider, receivedImpact, collisionDistance, collisionLayer);
            // transform.Translate(receivedImpact * Time.deltaTime);
        }

        public bool IsInvulnerable()
        {
            return isInvulnerable;
        }

        public float GetInvulnerableDuration()
        {
            return invulnerableDuration;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public float GetHealthPercent()
        {
            return health / maxHealth;
        }

        public float GetManaPercent()
        {
            return mana / maxMana;
        }

        public float GetMoney()
        {
            return money;
        }

        public void SetInvulnerable(bool value)
        {
            isInvulnerable = value;
        }

        public void HealHealth(float heal)
        {
            float newHealth = Mathf.Clamp(health + heal, 0, maxHealth);
            float oldHealth = health;
            health = newHealth;
            OnHealthChange.Invoke(oldHealth, newHealth);
        }

        public void ApplyImpact(Vector2 direction, float impact)
        {
            bool received = resistance < impact;
            if (!received) return;
            receivedImpact = direction * impact;
        }

        public bool IsDead()
        {
            return health == 0;
        }

        public bool TakeDamage(float damage, DamageType type)
        {
            if (isInvulnerable) return false;

            float defend = Random.Range(0f, defense);
            float realDamage = Mathf.Max(0, damage - defend);
            float oldHealth = health;
            float newHealth = Mathf.Max(0, health - realDamage);
            if (newHealth == health) return false;
            var offset = new Vector3(0.1f, 0, 0);
            WorldUIManager.Instance.ShowHit(transform.position + offset, realDamage);
            health = newHealth;
            OnHealthChange.Invoke(oldHealth, newHealth);
            StartCoroutine(Invulnerability());
            return true;
        }

        private IEnumerator Invulnerability()
        {
            isInvulnerable = true;
            yield return new WaitForSeconds(invulnerableDuration);
            receivedImpact = Vector2.zero;
            isInvulnerable = false;
        }

        public bool HasReceivedImpact()
        {
            return receivedImpact != Vector2.zero;
        }

        public Vector2 GetReceivedImpact()
        {
            return receivedImpact;
        }

        public float GetDamageForce(DamageType type)
        {
            float force = strength;
            switch (type)
            {
                case DamageType.Shoot:
                    force = shot;
                    break;
                case DamageType.Magic:
                    force = magic;
                    break;
            }
            return force;
        }

        public bool ApplyDamage(ICharacter enemy, DamageType type)
        {
            float force = GetDamageForce(type);
            float damage = Random.Range(0f, 1f) < criticalChance ? force * 2 : force;
            if (enemy.TakeDamage(damage, type))
            {
                if (enemy.IsDead())
                {
                    GainXP(enemy.GetXP());
                    return true;
                }
                return true;
            }
            return false;
        }

        public bool TryMove(BoxCollider2D boxCollider, Vector2 direction, float collisionDistance, LayerMask collisionLayer)
        {
            var position = boxCollider.bounds.center;
            var hit = Physics2D.BoxCast(position, boxCollider.bounds.size, 0, direction, collisionDistance, collisionLayer);
            if (hit.collider == null)
            {
                transform.Translate(direction * GetSpeed() * Time.deltaTime);
                return true;
            }
            return false;
        }

        public void AddMoney(float value)
        {
            float newMoney = Mathf.Clamp(money + value, 0, MAX_MONEY);
            OnMoneyChange.Invoke(money, newMoney);
            money = newMoney;
        }

        public void FillMana(float value)
        {
            float newMana = Mathf.Clamp(mana + value, 0, maxMana);
            OnManaChange.Invoke(money, newMana);
            mana = newMana;
        }

        public float GetXP()
        {
            return exp;
        }

        public void GainXP(float value)
        {
            float nextExp = exp + value;
            if (nextExp >= levelUpXp)
            {
                exp = nextExp - levelUpXp;
                levelUpXp *= levelUpIncreaseRate;
                level++;
            }
            else
            {
                exp = nextExp;
            }
        }

        public List<InventorySlot> GetInventoryItems()
        {
            return inventory.GetItems();
        }

        public InventorySlot? GetInventoryItemAt(int index)
        {
            return inventory.GetItem(index);
        }

        public bool TryAddToInventory(IInventoryItem item)
        {
            return inventory.TryAddItem(item);
        }
        public bool TryRemoveFromInventory(int index)
        {
            return inventory.TryRemoveItem(index);
        }

        public int GetAvailablePoints()
        {
            return level - levelPoints;
        }

        public int ApplyLevelPoint()
        {
            levelPoints--;
            return GetAvailablePoints();
        }

        public Vector2 GetDirection()
        {
            return direction;
        }
        public void SetDirection(Vector2 dir)
        {
            direction = dir;
        }

        public int GetLevel()
        {
            return level;
        }
        public float GetExperience()
        {
            return exp;
        }
        public float GetNextLevelExp()
        {
            return levelUpXp;
        }

        public float GetHealth()
        {
            return health;
        }
        public float GetMana()
        {
            return mana;
        }
        public float GetStrength()
        {
            return strength;
        }
        public float GetDefense()
        {
            return defense;
        }
        public float GetMagic()
        {
            return magic;
        }
        public float GetShot()
        {
            return shot;
        }



        public void IncreaseHealth()
        {
            health++;
            levelPoints++;
        }
        public void IncreaseMana()
        {
            mana++;
            levelPoints++;
        }
        public void IncreaseStrength()
        {
            strength++;
            levelPoints++;
        }
        public void IncreaseDefense()
        {
            defense++;
            levelPoints++;
        }
        public void IncreaseMagic()
        {
            magic++;
            levelPoints++;
        }
        public void IncreaseShot()
        {
            shot++;
            levelPoints++;
        }
    }
}
