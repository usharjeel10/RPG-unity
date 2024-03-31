using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class cinematicTrigger : MonoBehaviour
{
    private bool isTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isTrigger)
            {
                GetComponent<PlayableDirector>().Play();
                isTrigger = true;
            }
        }
    }
}