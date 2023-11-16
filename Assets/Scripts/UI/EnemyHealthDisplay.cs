using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealthDisplay : MonoBehaviour
{
    Fighter fighter;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (text == null) return;
        if(fighter.GetTarget() == null)
        {
            text.text = "N/A";
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
            Health health = fighter.GetTarget();

            text.text = health.health + "/" + health.enemyCurrenthealth;
        }

        
    }
}
