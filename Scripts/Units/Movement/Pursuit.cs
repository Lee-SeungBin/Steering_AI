using UnityEngine;

namespace UnityMovementAI
{
    [RequireComponent(typeof(SteeringBasics))]
    public class Pursuit : MonoBehaviour
    {
        public float maxPrediction = 1f;

        MovementAIRigidbody rb;
        SteeringBasics steeringBasics;

        void Awake()
        {
            rb = GetComponent<MovementAIRigidbody>();
            steeringBasics = GetComponent<SteeringBasics>();
        }

        public Vector3 GetSteering(MovementAIRigidbody target)
        {
            Vector3 displacement = target.Position - transform.position;
            float distance = displacement.magnitude;

            float speed = rb.Velocity.magnitude;

            float prediction;
            if (speed <= distance / maxPrediction)
            {
                prediction = maxPrediction;
            }
            else
            {
                prediction = distance / speed;
            }

            Vector3 explicitTarget = target.Position + target.Velocity * prediction;

            return steeringBasics.Seek(explicitTarget);
        }
    }
}