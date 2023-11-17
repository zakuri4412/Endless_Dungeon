using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingWrapper : MonoBehaviour
{
    const string defaultSaveFile = "save";
    GameManager gameManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }

    public void Delete()
    {
        GetComponent<SavingSystem>().SaveFileDelete(defaultSaveFile);
    }
}
