using UnityEngine;

namespace RPG.Control {

    public class PatrolPath : MonoBehaviour {
        private void OnDrawGizmos() {
            const float wayPointGizmoRadius = 0.3f;

            Gizmos.color = Color.yellow;
            for (int i = 0; i < transform.childCount; i++) {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWayPoint(i), wayPointGizmoRadius);
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
            }
        }

        public int GetNextIndex(int i) {
            return (i + 1) % transform.childCount;
        }

        public Vector3 GetWayPoint(int i) {
            return transform.GetChild(i).position;
        }
    }
}
