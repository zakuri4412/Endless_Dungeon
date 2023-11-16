using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
    {
        transform.position = target.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
        }
    }
}
