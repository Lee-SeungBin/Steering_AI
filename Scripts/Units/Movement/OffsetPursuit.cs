using UnityEngine;

namespace UnityMovementAI
{
    [RequireComponent(typeof(SteeringBasics))]
    public class OffsetPursuit : MonoBehaviour
    {
        public float maxPrediction = 1f;

        MovementAIRigidbody rb;
        SteeringBasics steeringBasics;

        void Awake()
        {
            rb = GetComponent<MovementAIRigidbody>();
            steeringBasics = GetComponent<SteeringBasics>();
        }

        public Vector3 GetSteering(MovementAIRigidbody target, Vector3 offset)
        {
            Vector3 targetPos;
            return GetSteering(target, offset, out targetPos);
        }

        public Vector3 GetSteering(MovementAIRigidbody target, Vector3 offset, out Vector3 targetPos)
        {
            Vector3 worldOffsetPos = target.Position + target.Transform.TransformDirection(offset);
            Vector3 displacement = worldOffsetPos - transform.position;
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
            targetPos = worldOffsetPos + target.Velocity * prediction;

            return steeringBasics.Arrive(targetPos);
        }
    }
}