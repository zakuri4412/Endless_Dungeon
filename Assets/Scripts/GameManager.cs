using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isNewGame = true;

    public void LoadGame()
    {
        DontDestroyOnLoad(gameObject);
        isNewGame = false;
    }

    public void NewGame()
    {
        DontDestroyOnLoad(gameObject);
        isNewGame = true;
    }




}
