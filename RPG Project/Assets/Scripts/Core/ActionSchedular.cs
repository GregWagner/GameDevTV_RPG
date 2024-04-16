using UnityEngine;

namespace RPG.Core {
    public class ActionSchedular : MonoBehaviour {
        IAction currentAction = null;
        public void StartAction(IAction action) {
            if (currentAction == action) {
                return;
            }
            if (currentAction != null) {
                currentAction.Cancel();
                print($"Cancelling {currentAction}");
            }
            currentAction = action;

        }

        public void CancelCurrentAction() {
            StartAction(null);
        }
    }
}
