using RPG.Characters;
using UnityEngine;

namespace RPG.Control {

    public class PlayerController : MonoBehaviour {

        private void Update() {
            if (Input.GetMouseButton(0)) {
                MoveToCursor();
            }
        }

        private void MoveToCursor() {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, 100f);
            if (hasHit) {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }
    }
}
