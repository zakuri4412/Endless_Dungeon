using UnityEngine;

public class CharacterController : MonoBehaviour, ISaveable
{
    Health health;
    public int currentFloor = -1;

    enum CursorType
    {
        None,
        Movement,
        Combat
    }

    [System.Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }

    [SerializeField] CursorMapping[] cursorMappings = null;
    public int GetCurrentFloor()
    {
        return currentFloor;
    }

    public void SetCurrentFloor(int floor)
    {
        currentFloor = floor;
    }
    private void Start()
    {
        health = GetComponent<Health>();
    }
    private void Update()
    {
        if (health.IsDead()) return;
        if (Combat()) return;
        if (MoveToCursor()) return;
        

        SetCursor(CursorType.None);
    }

    private bool MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            if (Input.GetMouseButtonDown(1))
            {

                GetComponent<Mover>().StartMoveAction(hit.point);
            }
            SetCursor(CursorType.Movement);
            return true;
        }
        return false;
    }

    private bool Combat()
    {
        RaycastHit[] hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
        foreach (RaycastHit hit in hits)
        {
            CombatTarget target = hit.transform.GetComponent<CombatTarget>();
            if (target == null || target.transform.gameObject.tag == "Player") continue;
            if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

            if (Input.GetMouseButtonDown(1))
            {
                GetComponent<Fighter>().Attack(target.gameObject);
            }
            SetCursor(CursorType.Combat);
            return true;
        }
        return false;
    }
    private void SetCursor(CursorType type)
    {
        CursorMapping mapping = GetCursorMapping(type);
        Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
    }

    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach (CursorMapping mapping in cursorMappings)
        {
            if (mapping.type == type)
            {
                return mapping;
            }
        }
        return cursorMappings[0];
    }
    public object CaptureState()
    {
        return currentFloor;
    }

    public void RestoreState(object state)
    {
        currentFloor = (int)state;
    }
}
