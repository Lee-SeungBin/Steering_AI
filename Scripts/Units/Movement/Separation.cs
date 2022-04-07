using UnityEngine;
using System.Collections.Generic;

namespace UnityMovementAI
{
    [RequireComponent(typeof(MovementAIRigidbody))]
    public class Separation : MonoBehaviour
    {
        public float sepMaxAcceleration = 25;

        public float maxSepDist = 1f;

        MovementAIRigidbody rb;

        void Awake()
        {
            rb = GetComponent<MovementAIRigidbody>();
        }

        public Vector3 GetSteering(ICollection<MovementAIRigidbody> targets)
        {
            Vector3 acceleration = Vector3.zero;

            foreach (MovementAIRigidbody r in targets)
            {
                Vector3 direction = rb.ColliderPosition - r.ColliderPosition;
                float dist = direction.magnitude;

                if (dist < maxSepDist)
                {
                    var strength = sepMaxAcceleration * (maxSepDist - dist) / (maxSepDist - rb.Radius - r.Radius);

                    direction.Normalize();
                    acceleration += direction * strength;
                }
            }

            return acceleration;
        }
    }
}