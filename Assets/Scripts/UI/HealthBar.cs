using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health healthComponent;
    [SerializeField] RectTransform foreground;

    private void Start()
    {
        healthComponent = GetComponentInParent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        foreground.localScale = new Vector3(healthComponent.GetHealthFraction(), 1, 1);
    }
}
