using UnityEngine;
namespace RPG.Core {
    public class ActionSchedular : MonoBehaviour {
        MonoBehaviour currentAction = null;
        public void StartAction(MonoBehaviour action) {
            if (currentAction == action) {
                return;
            }
            if (currentAction != null) {
                print($"Cancelling {currentAction}");
            }
            currentAction = action;

        }
    }
}
