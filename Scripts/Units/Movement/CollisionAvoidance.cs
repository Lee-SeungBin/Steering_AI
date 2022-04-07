using UnityEngine;
using System.Collections.Generic;

namespace UnityMovementAI
{
    [RequireComponent(typeof(MovementAIRigidbody))]
    public class CollisionAvoidance : MonoBehaviour
    {
        public float maxAcceleration = 15f;

        public float distanceBetween;

        MovementAIRigidbody rb;

        void Awake()
        {
            rb = GetComponent<MovementAIRigidbody>();
        }

        public Vector3 GetSteering(ICollection<MovementAIRigidbody> targets)
        {
            Vector3 acceleration = Vector3.zero;

            //처음 충돌할 대상 찻기
            float shortestTime = float.PositiveInfinity;

            MovementAIRigidbody firstTarget = null;
            float firstMinSeparation = 0, firstDistance = 0, firstRadius = 0;
            Vector3 firstRelativePos = Vector3.zero, firstRelativeVel = Vector3.zero;

            foreach (MovementAIRigidbody r in targets)
            {
                //충돌 시간 계산
                Vector3 relativePos = rb.ColliderPosition - r.ColliderPosition;
                Vector3 relativeVel = rb.RealVelocity - r.RealVelocity;
                float distance = relativePos.magnitude;
                float relativeSpeed = relativeVel.magnitude;

                if (relativeSpeed == 0)
                {
                    continue;
                }

                float timeToCollision = -1 * Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);

                Vector3 separation = relativePos + relativeVel * timeToCollision;
                float minSeparation = separation.magnitude;

                if (minSeparation > rb.Radius + r.Radius + distanceBetween)
                {
                    continue;
                }

                if (timeToCollision > 0 && timeToCollision < shortestTime)
                {
                    shortestTime = timeToCollision;
                    firstTarget = r;
                    firstMinSeparation = minSeparation;
                    firstDistance = distance;
                    firstRelativePos = relativePos;
                    firstRelativeVel = relativeVel;
                    firstRadius = r.Radius;
                }
            }

            if (firstTarget == null)
            {
                return acceleration;
            }

            //현재 충돌하고 있는경우 현재 위치로 조종
            if (firstMinSeparation <= 0 || firstDistance < rb.Radius + firstRadius + distanceBetween)
            {
                acceleration = rb.ColliderPosition - firstTarget.ColliderPosition;
            }
            //미래 위치 계산
            else
            {
                acceleration = firstRelativePos + firstRelativeVel * shortestTime;
            }

            acceleration.Normalize();
            acceleration *= maxAcceleration;

            return acceleration;
        }
    }
}