using RPG.Saving;
using sceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingWraper : MonoBehaviour
{
    const string DefaultSaveFile = "save";
    private float StartFadeTime = 0.4f;
    IEnumerator Start()
    {
        fader fade = FindObjectOfType<fader>();
        fade.ImediateFadeTime();
        yield return GetComponent<SavingSystem>().LoadLastScene(DefaultSaveFile);
        yield return fade.FadeOut(StartFadeTime);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }
    public void Load()
    {
        gameObject.GetComponent<SavingSystem>().Load(DefaultSaveFile);
    }
    public void Save()
    {
        gameObject.GetComponent<SavingSystem>().Save(DefaultSaveFile);
    }
}
