using UnityEngine;
using System.Collections;

namespace UnityMovementAI
{

    public class MovementAIRigidbody : MonoBehaviour
    {

        public float groundFollowDistance = 0.1f;

        public LayerMask groundCheckMask = Physics.DefaultRaycastLayers;

        public float slopeLimit = 80f;

        CircleCollider2D col2D;

        public float Radius
        {
            get
            {
                if (col2D != null)
                {
                    return Mathf.Max(rb2D.transform.localScale.x, rb2D.transform.localScale.y) * col2D.radius;
                }
                else
                {
                    return -1;
                }
            }
        }

        [System.NonSerialized]
        public Vector3 wallNormal = Vector3.zero;

        [System.NonSerialized]
        public Vector3 movementNormal = Vector3.up;

        Rigidbody2D rb2D;

        void Awake()
        {
            SetUp();
        }

        public void SetUp()
        {
            SetUpRigidbody();
            SetUpCollider();
        }

        void SetUpRigidbody()
        {
            this.rb2D = GetComponent<Rigidbody2D>();
        }

        void SetUpCollider()
        {
            CircleCollider2D col = rb2D.GetComponent<CircleCollider2D>();

            if (col != null)
            {
                col2D = col;
            }
        }

        void Start()
        {
            StartCoroutine(DebugDraw());
        }

        int count = 0;

        IEnumerator DebugDraw()
        {
            yield return new WaitForFixedUpdate();

            Vector3 origin = ColliderPosition;
            Debug.DrawLine(origin, origin + (Velocity.normalized), Color.red, 0f, false);

            count++;
            StartCoroutine(DebugDraw());
        }

        public Vector3 Position
        {
            get
            {
                return rb2D.position;
            }
        }

        public Vector3 ColliderPosition
        {
            get
            {
                return Transform.TransformPoint(col2D.offset);
            }
        }

        public Vector3 Velocity
        {
            get
            {
                    return rb2D.velocity;
            }

            set
            {
                    rb2D.velocity = value;
            }
        }

        public Vector3 RealVelocity
        {
            get
            {
                return (Vector3)rb2D.velocity;
            }
            set
            {
                rb2D.velocity = value;
            }
        }

        public Transform Transform
        {
            get
            {
                return rb2D.transform;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                    Quaternion r = Quaternion.identity;
                    r.eulerAngles = new Vector3(0, 0, rb2D.rotation);
                    return r;

            }

            set
            {
                rb2D.MoveRotation(value.eulerAngles.z);
            }
        }

        public float RotationInRadians
        {
            get
            {
                return rb2D.rotation * Mathf.Deg2Rad;
            }
        }

        public Vector3 RotationAsVector
        {
            get
            {
                return SteeringBasics.OrientationToVector(RotationInRadians);
            }
        }

    }
}