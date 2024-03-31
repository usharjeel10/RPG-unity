using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace sceneManagement
{
    public class fader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public void ImediateFadeTime()
        {
            canvasGroup.alpha = 1;
        }
        public IEnumerator FadeIn(float fadeTimer)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / fadeTimer;
                yield return null;
            }
        }
        public IEnumerator FadeOut(float fadeTimer)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / fadeTimer;
                yield return null;
            }
        }
    }
}