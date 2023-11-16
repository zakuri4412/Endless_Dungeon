using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Health health;
    Character character;
    [SerializeField] TMP_Text text;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void Update()
    {
        if (text == null) return;
        text.text = health.health + "/" + character.GetHealth();
    }
}
