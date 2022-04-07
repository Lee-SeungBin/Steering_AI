using UnityEngine;
using System.Collections.Generic;

namespace UnityMovementAI
{
    [RequireComponent(typeof(MovementAIRigidbody))]
    public class SteeringBasics : MonoBehaviour
    {
        [Header("General")]

        public float maxVelocity = 3.5f;
        public float maxAcceleration = 10f;
        public float turnSpeed = 20f;

        [Header("Arrive")]

        //타켓의 반지름
        public float targetRadius = 0.005f;
        //속도가 느려지기 시작할때 타켓으로부터 반경
        public float slowRadius = 1f;
        //타겟까지의 걸리는 시간
        public float timeToTarget = 0.1f;

        [Header("Look Direction Smoothing")]

        //원활한 방향 전환을 위해 사용
        public bool smoothing = true;
        public int numSamplesForSmoothing = 5;
        Queue<Vector3> velocitySamples = new Queue<Vector3>();

        MovementAIRigidbody rb;

        void Awake()
        {
            rb = GetComponent<MovementAIRigidbody>();
        }

        //선형 가속으로 현재 게임 오브젝트의 속도 업데이트
        public void Steer(Vector3 linearAcceleration)
        {
            rb.Velocity += linearAcceleration * Time.deltaTime;

            if (rb.Velocity.magnitude > maxVelocity)
            {
                rb.Velocity = rb.Velocity.normalized * maxVelocity;
            }
        }

        public Vector3 Seek(Vector3 targetPosition, float maxSeekAccel)
        {
            //방향
            Vector3 acceleration = targetPosition - transform.position;

            acceleration.Normalize();

            //가속도
            acceleration *= maxSeekAccel;

            return acceleration;
        }

        public Vector3 Seek(Vector3 targetPosition)
        {
            return Seek(targetPosition, maxAcceleration);
        }

        //타켓으로 방향을 바꿈
        public void LookWhereYoureGoing()
        {
            Vector3 direction = rb.Velocity;

            if (smoothing)
            {
                if (velocitySamples.Count == numSamplesForSmoothing)
                {
                    velocitySamples.Dequeue();
                }

                velocitySamples.Enqueue(rb.Velocity);

                direction = Vector3.zero;

                foreach (Vector3 v in velocitySamples)
                {
                    direction += v;
                }

                direction /= velocitySamples.Count;
            }

            LookAtDirection(direction);
        }

        public void LookAtDirection(Vector3 direction)
        {
            direction.Normalize();

            if (direction.sqrMagnitude > 0.001f)
            {
                    float toRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                    float rotation = Mathf.LerpAngle(rb.Rotation.eulerAngles.z, toRotation, Time.deltaTime * turnSpeed);

                    rb.Rotation = Quaternion.Euler(0, 0, rotation);
            }
        }

        public void LookAtDirection(Quaternion toRotation)
        {
                LookAtDirection(toRotation.eulerAngles.z);
        }

        //toRotation = 원하는 회적 각도
        public void LookAtDirection(float toRotation)
        {
                float rotation = Mathf.LerpAngle(rb.Rotation.eulerAngles.z, toRotation, Time.deltaTime * turnSpeed);

                rb.Rotation = Quaternion.Euler(0, 0, rotation);
        }

        public Vector3 Arrive(Vector3 targetPosition)
        {
            Debug.DrawLine(transform.position, targetPosition, Color.cyan, 0f, false);

            Vector3 targetVelocity = targetPosition - rb.Position;

            float dist = targetVelocity.magnitude;

            if (dist < targetRadius)
            {
                rb.Velocity = Vector3.zero;
                return Vector3.zero;
            }

            float targetSpeed;
            if (dist > slowRadius)
            {
                targetSpeed = maxVelocity;
            }
            else
            {
                targetSpeed = maxVelocity * (dist / slowRadius);
            }

            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            Vector3 acceleration = targetVelocity - rb.Velocity;

            acceleration *= 1 / timeToTarget;

            if (acceleration.magnitude > maxAcceleration)
            {
                acceleration.Normalize();
                acceleration *= maxAcceleration;
            }

            return acceleration;
        }

        public Vector3 Interpose(MovementAIRigidbody target1, MovementAIRigidbody target2)
        {
            Vector3 midPoint = (target1.Position + target2.Position) / 2;

            float timeToReachMidPoint = Vector3.Distance(midPoint, transform.position) / maxVelocity;

            Vector3 futureTarget1Pos = target1.Position + target1.Velocity * timeToReachMidPoint;
            Vector3 futureTarget2Pos = target2.Position + target2.Velocity * timeToReachMidPoint;

            midPoint = (futureTarget1Pos + futureTarget2Pos) / 2;

            return Arrive(midPoint);
        }

        public bool IsInFront(Vector3 target)
        {
            return IsFacing(target, 0);
        }

        public bool IsFacing(Vector3 target, float cosineValue)
        {
            Vector3 facing = transform.right.normalized;

            Vector3 directionToTarget = (target - transform.position);
            directionToTarget.Normalize();

            return Vector3.Dot(facing, directionToTarget) >= cosineValue;
        }

        public static Vector3 OrientationToVector(float orientation)
        {
            return new Vector3(Mathf.Cos(orientation), Mathf.Sin(orientation), 0);
        }

        public static float VectorToOrientation(Vector3 direction)
        {
            return Mathf.Atan2(direction.y, direction.x);
        }

        public static void DebugCross(Vector3 position, float size = 0.5f, Color color = default(Color), float duration = 0f, bool depthTest = true)
        {
            Vector3 xStart = position + Vector3.right * size * 0.5f;
            Vector3 xEnd = position - Vector3.right * size * 0.5f;

            Vector3 yStart = position + Vector3.up * size * 0.5f;
            Vector3 yEnd = position - Vector3.up * size * 0.5f;

            Vector3 zStart = position + Vector3.forward * size * 0.5f;
            Vector3 zEnd = position - Vector3.forward * size * 0.5f;

            Debug.DrawLine(xStart, xEnd, color, duration, depthTest);
            Debug.DrawLine(yStart, yEnd, color, duration, depthTest);
            Debug.DrawLine(zStart, zEnd, color, duration, depthTest);
        }
    }
}