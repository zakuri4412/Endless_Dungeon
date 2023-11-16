using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] Button button;
    const string filename = "save";
    // Start is called before the first frame update
    void Start()
    {
        button.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(File.Exists(Path.Combine(Application.persistentDataPath, filename + ".sav")))
        {
            button.interactable = true;
        }
    }
}
