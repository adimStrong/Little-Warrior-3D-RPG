using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;

    Transform target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget();
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        // movoing to the agent when not interacting with a interactables object

        agent.SetDestination(point);
    //    agent.Move(Vector3.forward * Time.deltaTime);
    }

    // this methong will work on interactables object when we use right click for example
    public void FollowTarget(Interactables newTarget)
    {
        // initializing stoping distance when we have are interacting with an object
        agent.stoppingDistance =newTarget.radius * .8f;
        // setting the update rotation of the agent into false
        agent.updateRotation = false;
        // setting the target into new transfor as we will use this to update the destination of the agent
        target = newTarget.interanctionTransform;

    }

    // call this method to stop player current movement and reset the settings to default
    public void StopFollowingTarget()
    {
        target = null;
        agent.updateRotation = true;

    }

    // call this method to face the interactable object
    void FaceTarget()
    {
        // initializing the rotation and normalizing it to get a absolute value
        Vector3 direction = (target.position - transform.position).normalized;

        // setting up the look directipn vector 3 no up and down rotation for now but can set the y if needed
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0, direction.z));

        // slerp from current rotaion to our new look rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);


    }
}
