using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine;

namespace RPG.Control {
    public class AIController : MonoBehaviour {
        [SerializeField] private float chaseDistance = 5.0f;
        [SerializeField] private float suspicionTime = 5.0f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float wayPointTolerence = 1.0f;
        [SerializeField] private float wayPointDwellTime = 2.0f;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWayPoint = Mathf.Infinity;
        private int currentWayPointIndex = 0;

        // cache references
        private Fighter fighter;
        private Health health;
        private Mover mover;
        private GameObject player;

        private void Start() {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;
        }

        private void Update() {
            if (health.IsDead()) {
                return;
            }
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player)) {
                AttackBehavior();
            } else if (timeSinceLastSawPlayer < suspicionTime) {
                SuspicionBehavior();
            } else {
                PatrolBehavior();
            }
            UpdateTimers();
        }

        private void UpdateTimers() {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWayPoint += Time.deltaTime;
        }

        private void PatrolBehavior() {
            Vector3 nextPosition = guardPosition;

            if (patrolPath != null) {
                if (AtWayPoint()) {
                    timeSinceArrivedAtWayPoint = 0.0f;
                    CycleWayPoint();
                }
                nextPosition = GetCurrentWayPoint();
            }
            if (timeSinceArrivedAtWayPoint > wayPointDwellTime) {
                mover.StartMoveAction(nextPosition);
            }
        }

        private Vector3 GetCurrentWayPoint() {
            return patrolPath.GetWayPoint(currentWayPointIndex);
        }

        private void CycleWayPoint() {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex); ;
        }

        private bool AtWayPoint() {
            float distanceToWayPoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWayPoint < wayPointTolerence;
        }

        private void SuspicionBehavior() {
            GetComponent<ActionSchedular>().CancelCurrentAction();
        }

        private void AttackBehavior() {
            timeSinceLastSawPlayer = 0.0f;
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer() {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
