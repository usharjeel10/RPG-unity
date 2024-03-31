//using RPG.combat;
//using xz = RPG.enemy.RPG.fight; // Alising of namespace
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.enemy.RPG.fight;
using RPG.combat;
//using other;
//using other2;
using UnityEngine.Analytics;
using RPG.core;

namespace RPG.control
{
    public class playerController : MonoBehaviour
    {
        Health health;
        private void Start()
        {
            health= GetComponent<Health>();
        }
        void Update()
        {
            /* if(Input.GetMouseButton(0))
             {
                 other.fig gig = new other.fig();
                 gig.Fight();
             }
             if(Input.GetMouseButton(1))
             {
                 other2.fig gig = new other2.fig();
                 gig.Fight();
             }*/


            if (health.IsDead()){ return; }
            if (interactWithCombat()) { return; }
            if (interactWithMouse()) { return; }
           
        }
        private bool interactWithCombat()
        {
            //RaycastHit[] _ray = Physics.RaycastAll(getMouseRay());
            foreach(RaycastHit ray in Physics.RaycastAll(getMouseRay()))
            {
               combat.combatTarget target = ray.transform.GetComponent<combat.combatTarget>();  // use the separate name space without intialize namespace in top
                if (target == null) continue;
                if (!GetComponent<fighter>().canAttack(target.gameObject)) 
                {
                    continue; 
                }
                    if(Input.GetMouseButton(0))
                    {
                        GetComponent<fighter>().Fight(target.gameObject);
                    }
                return true;
            }
            return false;
        }
        private bool interactWithMouse()
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit _hit;
                if (Physics.Raycast(getMouseRay(), out _hit, 100))
                {
                    GetComponent<playerMove>().startMoveAction(_hit.point,1);
                    return true;
                }
            }
            return false;
        }
        private static Ray getMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
