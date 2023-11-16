using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    [SerializeField] WeaponSO weapon;
    [SerializeField] float respawnTime = 3f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(weapon.GetLevelNeed() <= other.GetComponent<Stats>().GetLevel())
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
            else
            {
                Debug.Log("You dont have enought level to Equip");
            }
            
        }
    }

    private IEnumerator HideForSeconds(float seconds)
    {
        ShowPickup(false);
        yield return new WaitForSeconds(seconds);
        ShowPickup(true);
    }

    private void ShowPickup(bool shouldShow)
    {
        GetComponent<Collider>().enabled = shouldShow;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(shouldShow);
        }
    }
}
