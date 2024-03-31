using RPG.combat;
using RPG.core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.enemy  //we can also use namespace in hierarcy means in nested form
{
    namespace RPG.fight
    {
        public class fighter : MonoBehaviour,IAction
        {
            [HideInInspector] public static bool cinematicEnable;
            Health combatPos;
            private float timeBetweenAttack=1.21f;
            private float LastAttackTime;
            [SerializeField] private Transform RightHandweaponPos;
            [SerializeField] private Transform LeftHandweaponPos;
            [SerializeField] weapon defaultWeapon;
            weapon currentWeapon;
            private void Start()
            {
                EquipWeapon(defaultWeapon);
            }

           

            private void Update()
            {
                LastAttackTime += Time.deltaTime;
                
                if (combatPos == null) return;
                if (combatPos.IsDead()) return;
                if (isstop())
                {
                    GetComponent<playerMove>().cancel();
                    calculateAttackAnimation();
                }
                else
                {
                    GetComponent<playerMove>().Move(combatPos.transform.position,0.5f);
                }
            }
            public void EquipWeapon(weapon _weapon)
            {
                currentWeapon = _weapon;
                Animator anim = GetComponent<Animator>();
                _weapon.SpawnWeapon(anim, RightHandweaponPos, LeftHandweaponPos);
            }
            public bool canAttack(GameObject target)
            {
                if (target == null) return false;
                Health targetToTest = target.GetComponent<Health>();
                if (cinematicEnable)
                {
                    Debug.Log("tree");
                    targetToTest = null;
                }
                else
                {
                    Debug.Log("plant");
                }
                return (targetToTest != null && !targetToTest.IsDead());//enemy not die and target not null then return true
            }

            private void lookEnemy()
            {
                transform.LookAt(combatPos.transform.position);
            }

            public void calculateAttackAnimation()
            {
                lookEnemy();
                if (LastAttackTime > timeBetweenAttack)
                {
                    LastAttackTime = 0;
                    triggerAttack();
                }
            }
            private void triggerAttack()
            {
                GetComponent<Animator>().ResetTrigger("exitAttack");
                GetComponent<Animator>().SetTrigger("Attack");
            }

            void Hit()
            {
                    if (combatPos != null)
                    {
                        combatPos.takeDamage(currentWeapon.getDamage());
                    }
            }
            public void cancel()
            {
                ResetAttackAnimation();
                combatPos = null;
                GetComponent<playerMove>().cancel();
            }

            public void EnableCinematic()
            {
                cinematicEnable = true;
            }
            public void DisableCinematic()
            {
                cinematicEnable = false;
            }

            private void ResetAttackAnimation()
            {
                GetComponent<Animator>().ResetTrigger("Attack");
                GetComponent<Animator>().SetTrigger("exitAttack");
            }

            private bool isstop()
            {
                return Vector3.Distance(transform.position, combatPos.transform.position) < currentWeapon.getRange();
            }

            public void Fight(GameObject target)
            {
                GetComponent<actionScheduler>().playAction(this);
                combatPos = target.gameObject.GetComponent<Health>();
            }

        }
    }
}
/*
namespace other
{
    public class fig //: MonoBehaviour
    {
        public void Fight()//combatTarget target)
        {
            Debug.Log("yess");
        }
    }
}namespace other2
{
    public class fig //: MonoBehaviour
    {
        public void Fight()//combatTarget target)
        {
            Debug.Log("noo");
        }
    }
}*/