﻿using UnityEngine;

namespace RPG.Combat {
    public class Health : MonoBehaviour {
        [SerializeField] private float healthPoints = 100.0f;

        private bool isDead = false;

        public void TakeDamage(float damage) {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0) {
                Die();
            }
        }

        private void Die() {
            if (isDead) {
                return;
            }
            GetComponent<Animator>().SetTrigger("die");
            isDead = true;
        }
    }
}
