using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat {

    public class Fighter : MonoBehaviour, IAction {
        [SerializeField] private float weaponRange = 2.0f;
        [SerializeField] private float weaponDamage = 5.0f;
        [SerializeField] private float timeBetweenAttacks = 1.0f;

        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Update() {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) {
                return;
            }

            if (target.IsDead()) {
                return;
            }

            if (!GetIsInRange()) {
                GetComponent<Mover>().MoveTo(target.transform.position);
            } else {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }


        private void AttackBehavior() {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks) {
                TriggerAttack();
                timeSinceLastAttack = 0.0f;
            }
        }

        private void TriggerAttack() {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            // this will trigger the Hit() event
            GetComponent<Animator>().SetTrigger("attack");
        }

        // used to unarmed aninator trigger (animation event)
        private void Hit() {
            if (target == null) {
                return;
            }
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange() {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
        public bool CanAttack(GameObject combatTarget) {
            if (combatTarget == null) {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget) {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        public void Cancel() {
            StopAttack();
            target = null;
        }

        private void StopAttack() {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
