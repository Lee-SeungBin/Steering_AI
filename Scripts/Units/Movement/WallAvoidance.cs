using UnityEngine;
using System.Collections;

namespace UnityMovementAI
{
    [RequireComponent(typeof(SteeringBasics))]
    public class WallAvoidance : MonoBehaviour
    {
        public float maxAcceleration = 40f;

        public enum WallDetection { Raycast, Spherecast }

        public WallDetection wallDetection = WallDetection.Spherecast;

        public LayerMask castMask = Physics.DefaultRaycastLayers;

        public float wallAvoidDistance = 0.5f;
        public float mainWhiskerLen = 1.25f;
        public float sideWhiskerLen = 0.701f;
        public float sideWhiskerAngle = 45f;

        MovementAIRigidbody rb;
        SteeringBasics steeringBasics;

        void Awake()
        {
            rb = GetComponent<MovementAIRigidbody>();
            steeringBasics = GetComponent<SteeringBasics>();
        }

        public Vector3 GetSteering()
        {
            if (rb.Velocity.magnitude > 0.005f)
            {
                return GetSteering(rb.Velocity);
            }
            else
            {
                return GetSteering(rb.RotationAsVector);
            }
        }

        public Vector3 GetSteering(Vector3 facingDir)
        {
            Vector3 acceleration = Vector3.zero;

            GenericCastHit hit;

            if (!FindObstacle(facingDir, out hit))
            {
                return acceleration;
            }

            Vector3 targetPostition = hit.point + hit.normal * wallAvoidDistance;

            float angle = Vector3.Angle(rb.Velocity, hit.normal);
            if (angle > 165f)
            {
                Vector3 perp;
                perp = new Vector3(-hit.normal.y, hit.normal.x, hit.normal.z);

                targetPostition = targetPostition + (perp * Mathf.Sin((angle - 165f) * Mathf.Deg2Rad) * 2f * wallAvoidDistance);
            }

            return steeringBasics.Seek(targetPostition, maxAcceleration);
        }

        bool FindObstacle(Vector3 facingDir, out GenericCastHit firstHit)
        {
            facingDir.Normalize();

            Vector3[] dirs = new Vector3[3];
            dirs[0] = facingDir;

            float orientation = SteeringBasics.VectorToOrientation(facingDir);

            dirs[1] = SteeringBasics.OrientationToVector(orientation + sideWhiskerAngle * Mathf.Deg2Rad);
            dirs[2] = SteeringBasics.OrientationToVector(orientation - sideWhiskerAngle * Mathf.Deg2Rad);

            return CastWhiskers(dirs, out firstHit);
        }

        bool CastWhiskers(Vector3[] dirs, out GenericCastHit firstHit)
        {
            firstHit = new GenericCastHit();
            bool foundObs = false;

            for (int i = 0; i < dirs.Length; i++)
            {
                float dist = (i == 0) ? mainWhiskerLen : sideWhiskerLen;

                GenericCastHit hit;

                if (GenericCast(dirs[i], out hit, dist))
                {
                    foundObs = true;
                    firstHit = hit;
                    break;
                }
            }

            return foundObs;
        }

        bool GenericCast(Vector3 direction, out GenericCastHit hit, float distance = Mathf.Infinity)
        {
            bool result = false;
            Vector3 origin = rb.ColliderPosition;

                bool defaultQueriesStartInColliders = Physics2D.queriesStartInColliders;
                Physics2D.queriesStartInColliders = false;

                RaycastHit2D h;

                if (wallDetection == WallDetection.Raycast)
                {
                    h = Physics2D.Raycast(origin, direction, distance, castMask.value);
                }
                else
                {
                    h = Physics2D.CircleCast(origin, (rb.Radius * 0.5f), direction, distance, castMask.value);
                }

                result = (h.collider != null);
                hit = new GenericCastHit(h);

                Physics2D.queriesStartInColliders = defaultQueriesStartInColliders;

            return result;
        }

        struct GenericCastHit
        {
            public Vector3 point;
            public Vector3 normal;

            public GenericCastHit(RaycastHit h)
            {
                point = h.point;
                normal = h.normal;
            }

            public GenericCastHit(RaycastHit2D h)
            {
                point = h.point;
                normal = h.normal;
            }
        }
    }
}