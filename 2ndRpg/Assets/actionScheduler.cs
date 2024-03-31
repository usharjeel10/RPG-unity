using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.core
{
    public class actionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void playAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.cancel();
            }
            currentAction = action;

        }
        public void resetCurrentAction()
        {
            playAction(null);
        }
    }
}
