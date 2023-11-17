using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelText : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    WeaponEquip weaponEquip;
    // Start is called before the first frame update
    void Start()
    {
        weaponEquip = GetComponentInParent<WeaponEquip>();
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level Need: " + weaponEquip.GetLevelWeapon();
    }
}
