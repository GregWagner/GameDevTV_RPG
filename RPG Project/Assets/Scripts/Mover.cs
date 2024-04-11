using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour {
    [SerializeField] private Transform _target;

    private void Update() {
        UpdateAnimator();
    }

    private void UpdateAnimator() {
        // get the global velocity from the nav mesh agent
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;

        // convert it to a local velocity
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        // set animators blend value to be equal to  out desired
        // forward speed (on the Z-axis)
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    public void MoveTo(Vector3 destination) {
        var agent = GetComponent<NavMeshAgent>();
        agent.destination = destination;
    }
}
