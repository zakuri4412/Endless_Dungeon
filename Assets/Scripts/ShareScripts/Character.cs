using UnityEngine;

public enum CharacterClass
{
    Knight, Viking, Archer, Monster
}
public class Character : MonoBehaviour, ISaveable
{
    public CharacterSO character;
    public float GetHealth()
    {
        return character.health;
    }
    public void SetHealth(float health)
    {
        character.health = health;
    }


    public float GetAttackDamage()
    {
        return character.attackDamage;
    }
    public void SetAttackDamage(float damage)
    {
        character.attackDamage = damage;
    }

    public float GetAttackSpeed()
    {
        return character.attackSpeed;
    }

    public CharacterClass GetCharacterClass()
    {
        return character.characterClass;
    }

    public float GetAttackRange()
    {
        return character.attackRange;
    }

    public float GetLevelPoint()
    {
        return character.levelPoint;
    }

    public void SetLevelPoint(float levelPoint)
    {
        character.levelPoint = levelPoint;
    }
    public object CaptureState()
    {
        return character.health ;
    }

    public void RestoreState(object state)
    {
        character.health = (float)state;
    }


}
