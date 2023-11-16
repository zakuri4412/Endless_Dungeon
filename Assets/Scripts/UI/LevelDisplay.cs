using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    Stats stats;
    Character character;
    [SerializeField] TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("{0:0}", stats.GetLevel());
    }
}
