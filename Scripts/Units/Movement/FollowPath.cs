using UnityEngine;

namespace UnityMovementAI
{
    [RequireComponent(typeof(SteeringBasics))]
    public class FollowPath : MonoBehaviour
    {
        public float stopRadius = 0.005f;
        public float pathOffset = 0.71f;
        public float pathDirection = 1f;

        SteeringBasics steeringBasics;
        MovementAIRigidbody rb;

        void Awake()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            rb = GetComponent<MovementAIRigidbody>();
        }

        public Vector3 GetSteering(LinePath path)
        {
            return GetSteering(path, false);
        }

        public Vector3 GetSteering(LinePath path, bool pathLoop)
        {
            Vector3 targetPosition;
            return GetSteering(path, pathLoop, out targetPosition);
        }

        public Vector3 GetSteering(LinePath path, bool pathLoop, out Vector3 targetPosition)
        {

            if (path.Length == 1)
            {
                targetPosition = path[0];
            }
            else
            {
                float param = path.GetParam(transform.position, rb);

                if (!pathLoop)
                {
                    Vector3 finalDestination;
                    if (IsAtEndOfPath(path, param, out finalDestination))
                    {
                        targetPosition = finalDestination;

                        rb.Velocity = Vector3.zero;
                        return Vector3.zero;
                    }
                }

                param += pathDirection * pathOffset;

                targetPosition = path.GetPosition(param, pathLoop);
            }

            return steeringBasics.Arrive(targetPosition);
        }

        public bool IsAtEndOfPath(LinePath path)
        {

            if (path.Length == 1)
            {
                Vector3 endPos = path[0];
                return Vector3.Distance(rb.Position, endPos) < stopRadius;
            }

            else
            {
                Vector3 finalDestination;
                float param = path.GetParam(transform.position, rb);

                return IsAtEndOfPath(path, param, out finalDestination);
            }
        }

        bool IsAtEndOfPath(LinePath path, float param, out Vector3 finalDestination)
        {
            bool result;

            finalDestination = (pathDirection > 0) ? path[path.Length - 1] : path[0];

            if (param >= path.distances[path.Length - 2])
            {
                result = Vector3.Distance(rb.Position, finalDestination) < stopRadius;
            }

            else
            {
                result = false;
            }

            return result;
        }
    }
}