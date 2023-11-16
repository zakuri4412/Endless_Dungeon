using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavingSystem : MonoBehaviour
{
    public IEnumerator LoadLastScene(string saveFile, GameObject LoadingScreen, Image Loadingbar)
    {
        Dictionary<string, object> state = LoadFile(saveFile);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (state.ContainsKey("lastSceneBuildIndex"))
        {
            buildIndex = (int)state["lastSceneBuildIndex"];
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);
        LoadingScreen.SetActive(true);
        
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Loadingbar.fillAmount = progressValue;
            yield return null;
            
        }
        RestoreState(state);
        yield return null;

    }

    public void Save(string saveFile)
    {
        Dictionary<string, object> state = LoadFile(saveFile);
        CaptureState(state);
        SaveFile(saveFile, state);
    }

    public void Load(string saveFile)
    {
        RestoreState(LoadFile(saveFile));
    }

    public void Delete(string saveFile)
    {
        File.Delete(GetPathFromSaveFile(saveFile));
    }

    private Dictionary<string, object> LoadFile(string saveFile)
    {
        string path = GetPathFromSaveFile(saveFile);
        if (!File.Exists(path))
        {
            return new Dictionary<string, object>();
        }
        using (FileStream stream = File.Open(path, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }

    private void SaveFile(string saveFile, object state)
    {
        string path = GetPathFromSaveFile(saveFile);
        print("Saving to " + path);
        using (FileStream stream = File.Open(path, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }

    private void CaptureState(Dictionary<string, object> state)
    {
        foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
        }

        state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
    }

    private void RestoreState(Dictionary<string, object> state)
    {
        foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
        {
            string id = saveable.GetUniqueIdentifier();
            if (state.ContainsKey(id))
            {
                saveable.RestoreState(state[id]);
            }
        }
    }

    private string GetPathFromSaveFile(string saveFile)
    {
        return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
    }


    public void SaveFileDelete(string saveFile)
    {
        File.Delete(Path.Combine(Application.persistentDataPath, saveFile + ".sav"));
    }
}
