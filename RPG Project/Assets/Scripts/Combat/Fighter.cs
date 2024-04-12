using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour, IAction {
        [SerializeField] private float weaponRange = 2.0f;
        [SerializeField] private float weaponDamage = 5.0f;
        [SerializeField] private float timeBetweenAttacks = 1.0f;

        private Transform target;
        private float timeSinceLastAttack = 0.0f;

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) {
                return;
            }
            if (!GetIsInRange()) {
                GetComponent<Mover>().MoveTo(target.position);
            } else {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior() {
            if (timeSinceLastAttack > timeBetweenAttacks) {
                // this will trigger the Hit() event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0.0f;
            }
        }

        // used to unarmed aninator trigger (animation event)
        private void Hit() {
            Health health = target.GetComponent<Health>();
            health.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget) {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.transform;
        }
        public void Cancel() {
            target = null;
        }
    }
}
