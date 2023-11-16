using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpDisplay : MonoBehaviour
{
    Experience experience;
    [SerializeField] TMP_Text text;
    // Start is called before the first frame update
    void Awake()
    {
        experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("{0:0}", experience.GetEXP());
    }
}
