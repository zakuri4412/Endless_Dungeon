using UnityEngine;
using UnityEngine.AI;
public class Mover : MonoBehaviour, IAction,ISaveable
{
    NavMeshAgent navMeshAgent;
    Health health;
    Animator anim;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
    }
    void Update()
    {
        navMeshAgent.enabled = !health.IsDead();
        UpdateAnimator();


    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
        navMeshAgent.isStopped = false;

    }

    public void StartMoveAction(Vector3 destination)
    {
        GetComponent<ActionSchedule>().StartAction(this);
        MoveTo(destination);
    }

    public void Cancel()
    {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;
    }
    private void UpdateAnimator()
    {
        Vector3 velocity = navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        anim.SetFloat("Forward", speed);
    }

    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = (SerializableVector3)state;
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = position.ToVector();
        GetComponent<NavMeshAgent>().enabled = true;
    }
}
