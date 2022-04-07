using UnityEngine;

namespace UnityMovementAI
{
    public class PursuitUnit : MonoBehaviour
    {
        public MovementAIRigidbody target;

        SteeringBasics steeringBasics;
        Pursuit pursuit;

        void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            pursuit = GetComponent<Pursuit>();
        }

        void FixedUpdate()
        {
            Vector3 accel = pursuit.GetSteering(target);

            steeringBasics.Steer(accel);
            steeringBasics.LookWhereYoureGoing();
        }
    }
}