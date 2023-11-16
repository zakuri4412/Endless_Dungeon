using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour, ISaveable
{
    [SerializeField] float experiencePoints = 0;

    public void GainExperience(float experience)
    {
        experiencePoints += experience;
    }
    public float GetEXP()
    {
        return experiencePoints;
    }

    public void SetEXP(float exp)
    {
        experiencePoints = exp;
    }
    public object CaptureState()
    {
        return experiencePoints;
    }

    public void RestoreState(object state)
    {
        experiencePoints = (float)state;
    }
}
