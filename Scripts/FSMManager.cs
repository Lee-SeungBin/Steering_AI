using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UnityMovementAI
{
    public class FSMManager : MonoBehaviour
    {
        public GameObject seek, flee, arrive, pursuit, evade,
            wander1, wander2,
            offsetpursuit1, offsetpursuit2, offsetpursuit3, offsetpursuit4, offsetpursuit5, offsetpursuit6, interpose,
            obstacle, obstaclespawner, hide, followpath, wallavoid, wanavoid;
        public Text curtext;
        public enum Steering_States
        {
            Init, Idle, Seek, Flee, Arrive, Pursuit, Evade, Wander, Offset_Pursuit, Interpose, Hide, FollowPath, WallAvoid, WanAvoid
        }
        public enum Event_Name
        {
            Attack
        }
        public Steering_States State { get; private set; }
        public Event_Name Event { get; set; }
        void Awake()
        {
            this.State = Steering_States.Init;
        }

        void Start()
        {
            this.StartCoroutine(InitState());
        }

        IEnumerator InitState()
        {

            this.State = Steering_States.Idle;
            yield return null;

            ChangeState();
        }

        IEnumerator IdleState()
        {
            // Enter
            yield return null;

            while (this.State == Steering_States.Idle)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                switch (Input.inputString)
                {
                    case "1":
                        this.State = Steering_States.Seek;
                        break;
                    case "2":
                        this.State = Steering_States.Flee;
                        break;
                    case "3":
                        this.State = Steering_States.Arrive;
                        break;
                    case "4":
                        this.State = Steering_States.Pursuit;
                        break;
                    case "5":
                        this.State = Steering_States.Evade;
                        break;
                    case "6":
                        this.State = Steering_States.Wander;
                        break;
                    case "7":
                        this.State = Steering_States.Offset_Pursuit;
                        break;
                    case "8":
                        this.State = Steering_States.Interpose;
                        break;
                    case "9":
                        this.State = Steering_States.Hide;
                        break;
                    case "0":
                        this.State = Steering_States.FollowPath;
                        break;
                    case "A":
                        this.State = Steering_States.WallAvoid;
                        break;
                    case "a":
                        this.State = Steering_States.WallAvoid;
                        break;
                    case "B":
                        this.State = Steering_States.WanAvoid;
                        break;
                    case "b":
                        this.State = Steering_States.WanAvoid;
                        break;
                    default:
                        break;
                }
                yield return null;
            }

            // Exit
            ChangeState();
        }

        IEnumerator SeekState()
        {
            // Enter
            seek.SetActive(true);
            yield return null;

            while (this.State == Steering_States.Seek)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            seek.transform.position = Vector3.zero;
            seek.SetActive(false);
            ChangeState();
        }

        IEnumerator FleeState()
        {
            // Enter
            flee.SetActive(true);
            yield return null;

            while (this.State == Steering_States.Flee)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            flee.transform.position = Vector3.zero;
            flee.SetActive(false);
            ChangeState();
        }

        IEnumerator ArriveState()
        {
            // Enter
            arrive.SetActive(true);
            yield return null;

            while (this.State == Steering_States.Arrive)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            arrive.SetActive(false);
            arrive.transform.position = Vector3.zero;
            ChangeState();
        }

        IEnumerator PursuitState()
        {
            // Enter
            pursuit.SetActive(true);
            yield return null;

            while (this.State == Steering_States.Pursuit)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            pursuit.SetActive(false);
            pursuit.transform.position = Vector3.zero;
            ChangeState();
        }

        IEnumerator EvadeState()
        {
            // Enter
            evade.SetActive(true);
            yield return null;

            while (this.State == Steering_States.Evade)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            evade.SetActive(false);
            evade.transform.position = Vector3.zero;
            ChangeState();
        }

        IEnumerator WanderState()
        {
            // Enter
            wander1.SetActive(true);
            yield return null;

            while (this.State == Steering_States.Wander)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha6))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            wander1.SetActive(false);
            wander1.transform.position = Vector3.zero;
            ChangeState();
        }

        IEnumerator Offset_PursuitState()
        {
            // Enter
            offsetpursuit1.SetActive(true);
            offsetpursuit2.SetActive(true);
            offsetpursuit3.SetActive(true);
            offsetpursuit4.SetActive(true);
            offsetpursuit5.SetActive(true);
            offsetpursuit6.SetActive(true);
            yield return null;

            while (this.State == Steering_States.Offset_Pursuit)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha7))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            offsetpursuit1.SetActive(false);
            offsetpursuit2.SetActive(false);
            offsetpursuit3.SetActive(false);
            offsetpursuit4.SetActive(false);
            offsetpursuit5.SetActive(false);
            offsetpursuit6.SetActive(false);
            offsetpursuit1.transform.position = Vector3.zero;
            offsetpursuit2.transform.position = Vector3.zero;
            offsetpursuit3.transform.position = Vector3.zero;
            offsetpursuit4.transform.position = Vector3.zero;
            offsetpursuit5.transform.position = Vector3.zero;
            offsetpursuit6.transform.position = Vector3.zero;
            ChangeState();
        }

        IEnumerator InterposeState()
        {
            // Enter
            wander1.SetActive(true);
            wander2.SetActive(true);
            interpose.SetActive(true);
            yield return null;

            while (this.State == Steering_States.Interpose)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            wander1.SetActive(false);
            wander2.SetActive(false);
            interpose.SetActive(false);
            wander1.transform.position = Vector3.zero;
            wander2.transform.position = Vector3.zero;
            interpose.transform.position = Vector3.zero;
            ChangeState();
        }

        IEnumerator HideState()
        {
            // Enter
            hide.SetActive(true);
            obstacle.SetActive(true);
            obstaclespawner.SetActive(true);
            foreach (GameObject desob in GameObject.Find("ObstacleSpawner").GetComponent<Spawner>().desobj)
            {
                desob.SetActive(true);
            }
            yield return null;

            while (this.State == Steering_States.Hide)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha9))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            foreach (GameObject desob in GameObject.Find("ObstacleSpawner").GetComponent<Spawner>().desobj)
            {
                desob.SetActive(false);
            }
            hide.SetActive(false);
            obstacle.SetActive(false);
            obstaclespawner.SetActive(false);
            hide.transform.position = Vector3.zero;

            ChangeState();
        }

        IEnumerator FollowPathState()
        {
            // Enter
            followpath.SetActive(true);
            yield return null;

            while (this.State == Steering_States.FollowPath)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.Alpha0))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            followpath.SetActive(false);
            followpath.transform.position = Vector3.zero;
            ChangeState();
        }

        IEnumerator WallAvoidState()
        {
            // Enter
            wallavoid.SetActive(true);
            obstacle.SetActive(true);
            yield return null;

            while (this.State == Steering_States.WallAvoid)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.A))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            wallavoid.SetActive(false);
            obstacle.SetActive(false);
            wallavoid.transform.position = Vector3.zero;
            ChangeState();
        }

        IEnumerator WanAvoidState()
        {
            // Enter
            wanavoid.SetActive(true);
            obstacle.SetActive(true);
            obstaclespawner.SetActive(true);
            foreach (GameObject desob in GameObject.Find("ObstacleSpawner").GetComponent<Spawner>().desobj)
            {
                desob.SetActive(true);
            }
            yield return null;

            while (this.State == Steering_States.WanAvoid)
            {
                // Excute
                curtext.text = "현재 상태 : " + this.State;
                if (Input.GetKeyDown(KeyCode.B))
                {
                    this.State = Steering_States.Idle;
                }
                yield return null;
            }

            // Exit
            foreach (GameObject desob in GameObject.Find("ObstacleSpawner").GetComponent<Spawner>().desobj)
            {
                desob.SetActive(false);
            }
            wanavoid.SetActive(false);
            obstacle.SetActive(false);
            obstaclespawner.SetActive(false);
            wanavoid.transform.position = Vector3.zero;
            ChangeState();
        }
        public void ChangeState()
        {
            if (Random.Range(0, 10) == 0)
            {
                onEvent((int)Event_Name.Attack);
            }
            else
            {
                string methodName = State.ToString() + "State";
                System.Reflection.MethodInfo info = GetType().GetMethod(methodName,
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                StartCoroutine((IEnumerator)info.Invoke(this, null));
            }
        }

        public void onEvent(int eventname)
        {
            curtext.text = "이벤트 발생 -> " + this.Event;

            Invoke("ChangeState", 1.5f);
        }
    }
}