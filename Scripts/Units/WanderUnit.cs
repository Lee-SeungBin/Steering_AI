using UnityEngine;

namespace UnityMovementAI
{
    public class WanderUnit : MonoBehaviour
    {
        SteeringBasics steeringBasics;
        Wander wander;

        void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            wander = GetComponent<Wander>();
        }

        void FixedUpdate()
        {
            Vector3 accel = wander.GetSteering();

            steeringBasics.Steer(accel);
            steeringBasics.LookWhereYoureGoing();
        }
    }
}