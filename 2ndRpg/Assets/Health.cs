using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.core;
using RPG.Saving;

namespace RPG.core
{
    public class Health : MonoBehaviour ,ISaveable
    {
        [SerializeField]
        private int enemyHealth = 15;
        private bool isDie = false;
        public void takeDamage(int damage)
        {
            
            enemyHealth = Mathf.Max(enemyHealth - damage, 0);
            Die();
        }
        public bool IsDead()
        {
            return isDie;
        }
        private void Die()
        {
            Debug.Log("death");
            if (isDie) return;
            if (enemyHealth <= 0)
            {
                isDie = true;
                GetComponent<Animator>().SetTrigger("death");
                GetComponent<actionScheduler>().resetCurrentAction();
            }
        }

        public object CaptureState()
        {
            return enemyHealth;
        }

        public void RestoreState(object state)
        {
            enemyHealth = (int)state;
            if (enemyHealth <= 0)
            {
                Die();
            }
        }
    }
}
