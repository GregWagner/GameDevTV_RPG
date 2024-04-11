using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour {
    [SerializeField] private Transform _target;

    private void Update() {
        if (Input.GetMouseButton(0)) {
            MoveToCursor();
        }
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

    private void MoveToCursor() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, 100f);
        if (hasHit) {
            var agent = GetComponent<NavMeshAgent>();
            agent.destination = hit.point;
        }
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
    }
}
