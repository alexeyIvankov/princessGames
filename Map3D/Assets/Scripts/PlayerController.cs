using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] UnityChanControlScriptWithRgidBody anim;
    public ThirdPersonCharacter character;
    private Vector3 offset; //Private variable to store the offset distance between the player and camera
    public FixedJoystick leftJoystick;
    protected Rigidbody Rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        //agent.updateRotation = false;
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = camera.transform.position - character.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
          //  Debug.Log("HERE222222222");
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }


        else if (leftJoystick.input != Vector2.zero)
        {
            agent.ResetPath();
            // Debug.Log("HERE111111111111" + agent.destination);
            var input = new Vector3(leftJoystick.input.x, 0, leftJoystick.input.y);
            var vel = Quaternion.AngleAxis(221.597f, Vector3.up) * input * 5f;
            var velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
        //         agent.SetDestination(Vector3.Lerp(Rigidbody.position, velocity, 0.23f));
           Debug.Log("DESTINATION" + agent.destination);
           // Debug.Log("velocity" + velocity);
           Debug.Log("HERE2222222" + transform.position);

              agent.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
            transform.rotation =
                Quaternion.AngleAxis(
                    221.597f + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f,
                        Vector3.up), // поворот персонажа
                    Vector3.up);

            anim.Move(agent.velocity);
        }

        else if (leftJoystick.input == Vector2.zero && !(agent.remainingDistance > agent.stoppingDistance))
        {
            agent.velocity = Vector3.zero;
            // agent.SetDestination(transform.position);
            character.Move(Vector3.zero, false, false);
            anim.Move(Vector3.zero);
            agent.ResetPath();
        }
        else
        {
            Debug.Log("======================");
            character.Move(agent.velocity, false, false);
            anim.Move(agent.velocity);
        }

//        if (agent.velocity != Vector3.zero)
//        {
//            //Debug.Log("HERE!==========" + agent.desiredVelocity);
//            character.Move(agent.velocity, false, false);
//            anim.Move(agent.velocity);
//        }
//        else
//        {
//            //Debug.Log("HERE!------------");
//            character.Move(Vector3.zero, false, false);
//            anim.Move(agent.desiredVelocity);
//        }
    }


    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        camera.transform.position = character.transform.position + offset;
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}