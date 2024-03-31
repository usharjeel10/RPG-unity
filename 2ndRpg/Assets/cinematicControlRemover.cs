using RPG.control;
using RPG.core;
using RPG.enemy.RPG.fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class cinematicControlRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<PlayableDirector>().played += EnableControl;
        gameObject.GetComponent<PlayableDirector>().stopped += DisableControl;
    }
    void EnableControl(PlayableDirector pd)
    {
        GameObject playerAction = GameObject.FindWithTag("Player");
        playerAction.GetComponent<playerController>().enabled = false;
        playerAction.GetComponent<actionScheduler>().resetCurrentAction();
        playerAction.GetComponent<fighter>().EnableCinematic();
    }
    void DisableControl(PlayableDirector pd)
    {
        GameObject playerAction = GameObject.FindWithTag("Player");
        playerAction.GetComponent<playerController>().enabled = true;
        playerAction.GetComponent<fighter>().DisableCinematic();
    }

}
