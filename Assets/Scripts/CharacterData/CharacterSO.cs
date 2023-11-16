using UnityEngine;
[CreateAssetMenu(fileName = "CharacterData", menuName = "Character", order = 1)]
public class CharacterSO : ScriptableObject
{
    public float health;
    public float attackDamage;
    public float attackSpeed;
    public CharacterClass characterClass;
    public float attackRange;
    public float levelPoint;
}
