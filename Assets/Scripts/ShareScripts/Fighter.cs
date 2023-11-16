using UnityEngine;

public class Fighter : MonoBehaviour, IAction, ISaveable
{
    Health target;
    Animator anim;
    Character character;
    float timeLastAttack = 0f;
    float attackSpeed;

    [SerializeField] Transform handTransform = null;
    [SerializeField] WeaponSO weapon = null;

    [SerializeField] WeaponSO currentWeapon = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        character = GetComponent<Character>();
        if(currentWeapon == null)
        {
            EquipWeapon(weapon);
        }
    }
    private void Update()
    {
        if(handTransform != null)
        {
            if (handTransform.childCount == 0)
            {
                EquipWeapon(weapon);
            }
        }
        
        AttackBehavior();

    }

    private void AttackBehavior()
    {
        float attackBoots = 10f;
        if (currentWeapon != null)
        {
            attackBoots += currentWeapon.GetAttackSpeedBoost();
        }
        attackSpeed = (character.GetAttackSpeed() / ((attackBoots / 100) + 1));
        timeLastAttack += Time.deltaTime;

        if (target == null) return;
        if (target.IsDead()) return;

        if (!InRange())
        {
            GetComponent<Mover>().MoveTo(target.transform.position);
        }
        else
        {
            GetComponent<Mover>().Cancel();
            AttackAnim();
        }
    }

    public void EquipWeapon(WeaponSO weaponEquip)
    {
        if (weaponEquip == null) return;
        currentWeapon = weaponEquip;
        weaponEquip.Spawn(handTransform, anim);
        
    }

    public Health GetTarget()
    {
        return target;
    }

    private bool InRange()
    {
        float weaponRange = 0;
        if(currentWeapon != null)
        {
            weaponRange += currentWeapon.GetWeaponRange();
        }
        return Vector3.Distance(transform.position, target.transform.position) <= (character.GetAttackRange() + weaponRange);
    }

    private void AttackAnim()
    {
        transform.LookAt(target.transform.position);
        if (timeLastAttack > attackSpeed)
        {
            TriggerAttack();
            timeLastAttack = 0f;
        }

    }
    public void Cancel()
    {
        TriggerStopAttack();
        target = null;
    }

    public bool CanAttack(GameObject target)
    {
        if (target == null) return false;
        Health targetAlive = target.GetComponent<Health>();
        return targetAlive != null && !targetAlive.IsDead();
    }
    public void Attack(GameObject target)
    {
        GetComponent<ActionSchedule>().StartAction(this);
        this.target = target.GetComponent<Health>();
    }

    public void Hit()
    {
        if (target == null) return;
        float damage = character.GetAttackDamage();
        if (character.GetCharacterClass() == CharacterClass.Monster)
        {
            CharacterController characterController = FindObjectOfType<CharacterController>();
            damage += characterController.GetCurrentFloor() * 10;
        }
        if (currentWeapon != null)
        {
            damage += currentWeapon.GetWeaponDamage();
        }
        target.TakeDamage(damage,gameObject);
    }

    private void TriggerAttack()
    {
        anim.SetTrigger("Punch");
        anim.ResetTrigger("StopAttack");
    }

    private void TriggerStopAttack()
    {
        anim.SetTrigger("StopAttack");
        anim.ResetTrigger("Punch");
    }

    public object CaptureState()
    {
        return currentWeapon.name;
    }

    public void RestoreState(object state)
    {
        string weaponName = (string)state;
        WeaponSO weapon = Resources.Load<WeaponSO>(weaponName);
        EquipWeapon(weapon);
    }
}
