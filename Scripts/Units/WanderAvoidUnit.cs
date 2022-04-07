using UnityEngine;

namespace UnityMovementAI
{
    public class WanderAvoidUnit : MonoBehaviour
    {
        SteeringBasics steeringBasics;
        Wander wander;
        CollisionAvoidance colAvoid;

        NearSensor colAvoidSensor;

        void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            wander = GetComponent<Wander>();
            colAvoid = GetComponent<CollisionAvoidance>();

            colAvoidSensor = transform.Find("ColAvoidSensor").GetComponent<NearSensor>();
        }

        void FixedUpdate()
        {
            Vector3 accel = colAvoid.GetSteering(colAvoidSensor.targets);

            if (accel.magnitude < 0.005f)
            {
                accel = wander.GetSteering();
            }

            steeringBasics.Steer(accel);
            steeringBasics.LookWhereYoureGoing();
        }
    }
}