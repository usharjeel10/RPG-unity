using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.core
{
    public class followPlayer : MonoBehaviour
    {
        [SerializeField] private Transform Target;
        void Start()
        {

        }
        // Update is called once per frame
        private void LateUpdate()
        {
            this.transform.position = Target.transform.position;// *Time.deltaTime;   
        }
    }
}
