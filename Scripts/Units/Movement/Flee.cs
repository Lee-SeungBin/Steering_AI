using UnityEngine;

namespace UnityMovementAI
{
    [RequireComponent(typeof(MovementAIRigidbody))]
    public class Flee : MonoBehaviour
    {
        public float panicDist = 3.5f;

        public bool decelerateOnStop = true;

        public float maxAcceleration = 10f;

        public float timeToTarget = 0.1f;

        MovementAIRigidbody rb;

        void Awake()
        {
            rb = GetComponent<MovementAIRigidbody>();
        }

        public Vector3 GetSteering(Vector3 targetPosition)
        {
            //방향
            Vector3 acceleration = transform.position - targetPosition;

            //거리 멀면  flee 중지
            if (acceleration.magnitude > panicDist)
            {
                //감속
                if (decelerateOnStop && rb.Velocity.magnitude > 0.001f)
                {
                    acceleration = -rb.Velocity / timeToTarget;

                    if (acceleration.magnitude > maxAcceleration)
                    {
                        acceleration = GiveMaxAccel(acceleration);
                    }

                    return acceleration;
                }
                else
                {
                    rb.Velocity = Vector3.zero;
                    return Vector3.zero;
                }
            }

            return GiveMaxAccel(acceleration);
        }

        Vector3 GiveMaxAccel(Vector3 v)
        {
            v.Normalize();
            v *= maxAcceleration;

            return v;
        }
    }
}