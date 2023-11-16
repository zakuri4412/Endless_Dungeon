using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons", order = 0)]
public class WeaponSO : ScriptableObject
{
    [SerializeField] AnimatorOverrideController animatorOverride = null;
    [SerializeField] GameObject weaponPrefab = null;
    [SerializeField] float weaponDamage;
    [SerializeField] float attackSpeedBoost;
    [SerializeField] float weaponRange;
    [SerializeField] float levelNeed;
    const string weaponName = "Weapon";
    public void Spawn(Transform handTransform, Animator animator)
    {
        DestroyOldWeapon(handTransform);
        if (animator == null) return;
        var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        if (weaponPrefab != null)
        {
            GameObject weapon = Instantiate(weaponPrefab, handTransform);
            weapon.name = weaponName;
        }
        if (animatorOverride != null)
        {
            animator.runtimeAnimatorController = animatorOverride;
        }
        else if (overrideController != null)
        {
            animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
        }
    }
    private void DestroyOldWeapon(Transform rightHand)
    {
        Transform oldWeapon = rightHand.Find(weaponName);
        if (oldWeapon == null) return;
        oldWeapon.name = "DESTROYING";
        Destroy(oldWeapon.gameObject);
    }


    public float GetWeaponDamage()
    {
        return weaponDamage;
    }

    public float GetAttackSpeedBoost()
    {
        return attackSpeedBoost;
    }

    public float GetWeaponRange()
    {
        return weaponRange;
    }

    public float GetLevelNeed()
    {
        return levelNeed;
    }
}
