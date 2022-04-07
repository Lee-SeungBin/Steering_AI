using UnityEngine;

namespace UnityMovementAI
{
    [RequireComponent(typeof(Flee))]
    public class Evade : MonoBehaviour
    {
        //최대 예측시간
        public float maxPrediction = 1f;

        Flee flee;

        void Awake()
        {
            flee = GetComponent<Flee>();
        }

        public Vector3 GetSteering(MovementAIRigidbody target)
        {
            //표적까지의 거리
            Vector3 displacement = target.Position - transform.position;
            float distance = displacement.magnitude;

            float speed = target.Velocity.magnitude;

            float prediction;
            if (speed <= distance / maxPrediction)
            {
                prediction = maxPrediction;
            }
            else
            {
                prediction = distance / speed;
                //예상 위치 계산
                prediction *= 0.9f;
            }

            Vector3 explicitTarget = target.Position + target.Velocity * prediction;

            return flee.GetSteering(explicitTarget);
        }
    }
}