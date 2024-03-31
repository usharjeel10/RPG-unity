using RPG.core;
using RPG.enemy.RPG.fight;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RPG.control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float range = 5,PatrolWayPointRange=1.0f,TimeToMoveNextWayPoint=4;
        private float currentRange;
        GameObject player;
        fighter _fighter;
        Health health;
        playerMove move;
        actionScheduler changeAction;
        private Vector3 gaurdPosition;
        private float timeSinceLastSawPlayer=Mathf.Infinity;
        private float timeSinceLastStandingPatrolPoint=Mathf.Infinity;
        [SerializeField] wayPointBehaviour patrolPoint;
        private int currectWayPointIndex=0;
        [Range(0, 1)]
        private float patrolSpeedFraction=0.2f;
        private void Start()
        {
            changeAction = GetComponent<actionScheduler>();
            move =GetComponent<playerMove>();
            health = GetComponent<Health>();
            _fighter = GetComponent<fighter>();
            player = GameObject.FindWithTag("Player");
            gaurdPosition=transform.position;
        }
        private void Update()
        {

            if (health.IsDead()) return;
           if (findRange()&& _fighter.canAttack(player))
           {
                timeSinceLastSawPlayer = 0;
                _fighter.Fight(player);
           }
           else if (timeSinceLastSawPlayer<range)
            {
                suspisiousMethod();
            }
            else
            {
                petrolBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void petrolBehaviour()
        {
            Vector3 nextPos = gaurdPosition;
            if (patrolPoint!=null)
            {
                if (AtWayPoint())
                {
                    timeSinceLastStandingPatrolPoint += Time.deltaTime;
                    if(timeSinceLastStandingPatrolPoint > range)
                    {
                        timeSinceLastStandingPatrolPoint= 0;
                        cycleWayPoint();
                    }
                }
                nextPos = getNextPetrolDirection();
            }
          //  Debug.Log(patrolSpeedFraction);
            move.startMoveAction(nextPos,patrolSpeedFraction);
        }

        private bool AtWayPoint()
        {
            float dir = Vector3.Distance(this.transform.position, getNextPetrolDirection());
            return dir < PatrolWayPointRange;
        }

        private void cycleWayPoint()
        {
           currectWayPointIndex= patrolPoint.transform.GetComponent<wayPointBehaviour>().getNextIndex(currectWayPointIndex);
        }

        private Vector3 getNextPetrolDirection()
        {
            return patrolPoint.transform.GetComponent<wayPointBehaviour>().getWaypoint(currectWayPointIndex);
        }

        private void suspisiousMethod()
        {
            changeAction.resetCurrentAction();
        }

        private bool findRange()
        {
            currentRange = Vector3.Distance(this.transform.position,player.transform.position);
            return currentRange <= range;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}