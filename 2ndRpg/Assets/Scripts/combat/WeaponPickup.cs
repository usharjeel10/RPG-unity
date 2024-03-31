using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.enemy.RPG.fight;
namespace RPG.combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] weapon _weapon;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<fighter>().EquipWeapon(_weapon);
                Destroy(gameObject);
            
            }
        }
    }
}