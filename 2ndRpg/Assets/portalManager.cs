using RPG.control;
using RPG.enemy.RPG.fight;
using sceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
namespace RPG.core
{
    public class portalManager : MonoBehaviour
    {
        enum destinationIdentifier
        {
            A, B
        }
        [SerializeField] private destinationIdentifier destination;
        [SerializeField] private int sceneToLoad = 1;
        [SerializeField] private Transform spawnPoint;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(Transitopn());
            }
        }
        private IEnumerator Transitopn()
        {
            DontDestroyOnLoad(this);
            fader fade = FindObjectOfType<fader>();
            //player.transform.GetComponent<NavMeshAgent>().isStopped = true;
            yield return fade.FadeIn(2);
            SavingWraper wrapper = FindObjectOfType<SavingWraper>();
            wrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            wrapper.Load();
            portalManager portal = GetOtherPortal();
            updatePlayer(portal);
            wrapper.Save();
            yield return new WaitForSeconds(0.5f);
          //  player.transform.GetComponent<NavMeshAgent>().isStopped = false;
            yield return fade.FadeOut(1);
            Destroy(gameObject);
        }
        private void updatePlayer(portalManager portal)
        {
            GameObject player = GameObject.FindWithTag("Player");
             if (player != null)
            {
                player.GetComponent<NavMeshAgent>().Warp(portal.spawnPoint.position);
                player.transform.rotation = portal.spawnPoint.rotation;
            }
        }
        private portalManager GetOtherPortal()
        {
            foreach (portalManager portal in FindObjectsOfType<portalManager>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    }
}