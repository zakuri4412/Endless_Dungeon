using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScence : MonoBehaviour
{
    Health health;
    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.IsDead())
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }
}
