using UnityEngine;

namespace UnityMovementAI
{
    [RequireComponent(typeof(SteeringBasics))]
    public class Wander : MonoBehaviour
    {
        public float wanderRadius = 1.2f;
        public float wanderDistance = 2f;
        public float wanderJitter = 40f;

        Vector3 wanderTarget;

        SteeringBasics steeringBasics;

        void Awake()
        {
            steeringBasics = GetComponent<SteeringBasics>();
        }

        void Start()
        {

            float theta = Random.value * 2 * Mathf.PI;
                wanderTarget = new Vector3(wanderRadius * Mathf.Cos(theta), wanderRadius * Mathf.Sin(theta), 0f);
        }

        public Vector3 GetSteering()
        {
            float jitter = wanderJitter * Time.deltaTime;

                wanderTarget += new Vector3(Random.Range(-1f, 1f) * jitter, Random.Range(-1f, 1f) * jitter, 0f);

            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;

            Vector3 targetPosition = transform.position + transform.right * wanderDistance + wanderTarget;


            return steeringBasics.Seek(targetPosition);
        }
    }
}