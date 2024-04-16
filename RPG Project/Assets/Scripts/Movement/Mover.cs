using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement {
    public class Mover : MonoBehaviour, IAction {
        [SerializeField] private Transform _target;

        private NavMeshAgent navMeshAgent;
        private Health health;

        private void Start() {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        private void Update() {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator() {
            // get the global velocity from the nav mesh agent
            Vector3 velocity = navMeshAgent.velocity;

            // convert it to a local velocity
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);

            // set animators blend value to be equal to  out desired
            // forward speed (on the Z-axis)
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        public void StartMoveAction(Vector3 destination) {
            GetComponent<ActionSchedular>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination) {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel() {
            navMeshAgent.isStopped = true;
        }
    }
}
