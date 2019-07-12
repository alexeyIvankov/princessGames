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
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera


    // Start is called before the first frame update
    void Start()
    {
        //agent.updateRotation = false;
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = camera.transform.position - character.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
           if( Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            Debug.Log("HERE!");
            character.Move(agent.desiredVelocity, false, false);
            anim.Move(agent.desiredVelocity);
        }
        else
        {
            Debug.Log("HERE!==========");
            character.Move(Vector3.zero, false, false);
            anim.Move(agent.desiredVelocity);
        }
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
