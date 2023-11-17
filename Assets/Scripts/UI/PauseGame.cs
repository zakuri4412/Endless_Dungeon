using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] bool isPaused;
    SavingWrapper wrapper;
    private void Start()
    {
        wrapper = FindObjectOfType<SavingWrapper>();
        canvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }

    }

    public void SaveGame()
    {
        wrapper.Save();
    }

    public void LoadGame()
    {
        wrapper.Load();
    }
    void Pause()
    {
        canvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void ResumeGame()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }


}
