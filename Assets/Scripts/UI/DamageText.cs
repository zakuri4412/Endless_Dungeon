using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] TMP_Text damageText;
    public void DestroyText()
    {
        Destroy(gameObject);
    }

    public void SetValue(float amount)
    {
        damageText.text = string.Format("{0:0}", amount);
    }
}
