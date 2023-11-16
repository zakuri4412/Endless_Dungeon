using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, ISaveable
{
    [Range(1, 99)]
    [SerializeField] public int startingLevel = 1;
     float baseHealth = 150f;
     float baseAttack = 10f;
    float baseLevelPoint = 10f;
    Character character;
    [SerializeField] GameObject levelUpEffect = null;
    private void Awake()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        character = GetComponent<Character>();
        if (gameManager.isNewGame)
        {
            character.SetHealth(baseHealth)  ;
            character.SetAttackDamage(baseAttack);
            character.SetLevelPoint(baseLevelPoint);
            startingLevel = 1;
        }
    }


    private void Update()
    {
        if (gameObject.tag == "Player")
        {
            print(GetLevel());
        }
    }


    public float GetLevel()
    {
        Experience experience = GetComponent<Experience>();
        if (experience == null) return startingLevel;
        Character character = GetComponent<Character>();
        if (character == null) return startingLevel;
        float currentXP = experience.GetEXP();
        float levelUpPoint = character.GetLevelPoint();
        if(currentXP >= levelUpPoint)
        {
            startingLevel++;
            float health = Mathf.Round(character.GetHealth() + (character.GetHealth() * 0.3f));
            character.SetHealth(health);
            float attack = character.GetAttackDamage() + 2f;
            character.SetAttackDamage(attack);
            float newLevelPoint = character.GetLevelPoint() + (startingLevel * 10);
            character.SetLevelPoint(newLevelPoint);
            experience.SetEXP(0f);
            LevelUpEffect();
        }
        return startingLevel;
    }

    private void LevelUpEffect()
    {
        Instantiate(levelUpEffect, transform);
    }


    public object CaptureState()
    {
        return startingLevel;
    }

    public void RestoreState(object state)
    {
        startingLevel = (int)state;
    }
}
