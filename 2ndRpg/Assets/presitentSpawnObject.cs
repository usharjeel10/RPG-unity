using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class presitentSpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject fadePrefab;
    public static bool canFade = false;
    private void Awake()
    {
        if (canFade) return;
        spawnFade();
    }
    private void spawnFade()
    {
        canFade= true;
        GameObject fadeobj=Instantiate(fadePrefab);
        DontDestroyOnLoad(fadeobj);
    }
}
