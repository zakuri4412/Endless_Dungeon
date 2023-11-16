using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
[System.Serializable]
public class TakeDamageEvent : UnityEvent<float>
{
}
public class Health : MonoBehaviour, ISaveable
{
    Character character;
    Animator anim;
    public float health;
    bool isDead;
    float ExpPoint = 10f;
    public float enemyCurrenthealth;
    CharacterController characterController;

    [SerializeField] TakeDamageEvent takeDamage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        character = GetComponent<Character>();
        characterController = FindObjectOfType<CharacterController>();
        health = character.GetHealth();

    }

    private void Start()
    {
        StartCoroutine(UpdateHealth());

    }

    private IEnumerator UpdateHealth()
    {
        yield return new WaitForSeconds(0.1f);
        if (character.GetCharacterClass() == CharacterClass.Monster)
        {

            health += characterController.GetCurrentFloor() * 50;
            ExpPoint += characterController.GetCurrentFloor() * 2;
            enemyCurrenthealth = health;
        }
        if (SceneManager.GetActiveScene().name == "Home")
        {
            health = character.GetHealth();
            isDead = false;
            anim.SetTrigger("BackToLive");
        }
    }


    public float GetPercentage()
    {
        return 100 * GetHealthFraction();
    }

    public float GetHealthFraction()
    {
        return (health / character.GetHealth());
    }
    public void TakeDamage(float damage, GameObject player)
    {
        health = Mathf.Max(health - damage, 0);
        if (health == 0)
        {
            
            Die();
            GetEXP(player);
        }
        else
        {
            takeDamage.Invoke(damage);
        }
    }
    private void GetEXP(GameObject instigator)
    {
        Experience experience = instigator.GetComponent<Experience>();
        if (experience == null) return;
        experience.GainExperience(ExpPoint);
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        anim.SetTrigger("Die");
        GetComponent<ActionSchedule>().CancelAction();
    }

    public bool IsDead()
    {
        return isDead;
    }

    public object CaptureState()
    {
        return health;
    }

    public void RestoreState(object state)
    {
        health = (float)state;
        if (health == 0)
        {
            Die();
        }
    }

}
