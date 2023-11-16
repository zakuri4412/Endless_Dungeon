using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] float waypointTime = 3f;
    [SerializeField] PatrolPath path;

    Fighter fighter;
    GameObject player;
    Health health;
    Mover mover;
    Vector3 rootPos;
    float timeSinceLastSeen = 0f;
    float timeSinceLastWaypoint = 0f;
    int currentPoint = 0;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        mover = GetComponent<Mover>();
        rootPos = transform.position;
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (health.IsDead()) return;
        if (InAttackRange() && fighter.CanAttack(player))
        {
            timeSinceLastSeen = 0f;
            fighter.Attack(player);
        }
        else if (timeSinceLastSeen < suspicionTime)
        {
            GetComponent<ActionSchedule>().CancelAction();
        }
        else
        {
            Patrol();
        }
        timeSinceLastSeen += Time.deltaTime;
        timeSinceLastWaypoint += Time.deltaTime;
    }

    private void Patrol()
    {
        Vector3 nextPos = rootPos;
        if (path != null)
        {
            if (ToWayPoint())
            {
                timeSinceLastWaypoint = 0f;
                currentPoint = path.GetNextIndex(currentPoint);
            }
            nextPos = path.GetWayPoint(currentPoint);
        }
        if (timeSinceLastWaypoint > waypointTime)
        {
            mover.StartMoveAction(nextPos);
        }

    }

    private bool ToWayPoint()
    {
        return Vector3.Distance(transform.position, path.GetWayPoint(currentPoint)) < 1f;
    }

    private bool InAttackRange()
    {

        return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
