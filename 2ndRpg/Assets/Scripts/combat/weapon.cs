using UnityEngine;
namespace RPG.combat
{
    [CreateAssetMenu(fileName = "weapon",menuName ="weapons/make new weapon",order =0)]
    public class weapon : ScriptableObject
    {
        [SerializeField] private GameObject SwordPrefab;
        [SerializeField] private AnimatorOverrideController weaponAnimation = null;
        [SerializeField] private int AttackDamage;
        [SerializeField] private float AttackRange;
        [SerializeField] private bool isRightHand;
        private Transform weaponHandPos;
        public void SpawnWeapon(Animator anim, Transform RightHand,Transform LeftHand)
        {
            if (SwordPrefab==null) { return; }
            if (isRightHand) { weaponHandPos = RightHand; }
            else weaponHandPos = LeftHand;
            Instantiate(SwordPrefab, weaponHandPos);
            if (weaponAnimation == null) { return; }
            anim.runtimeAnimatorController = weaponAnimation;
        }
        public int getDamage()
        {
            return AttackDamage;
        }
        public float getRange()
        {
            return AttackRange;
        }
    }
}