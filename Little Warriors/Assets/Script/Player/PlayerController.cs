using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementMask;

    public Interactables focus;

    Camera cam;

    PlayerMotor playerMotor;

    private void Start()
    {
        cam = Camera.main;
        playerMotor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (Input.GetMouseButtonDown(0))
        {
            // generate a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
         
            if(Physics.Raycast(ray, out hit, 100 , movementMask))
            {
                // move player when we hit or click something

                playerMotor.MoveToPoint(hit.point);
                // stop focusing any object
                RemoveFocus();

            }

        }


        if (Input.GetMouseButtonDown(1))
        {
            // generate a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
               Interactables interactables = hit.collider.GetComponent<Interactables>();
              // check if we hit interactable if we did set it our focus
              if(interactables != null)
                {
                    SetFocus(interactables);
                }
            }

        }
    }
    // this methong will use to get the position of interactable object and setting the destination of the agent
    void SetFocus(Interactables newFocus)
    {
        if(newFocus != focus)
        {
            if (focus != null)
                focus.Defocused();
         
            focus = newFocus;
            playerMotor.FollowTarget(newFocus);
        }


        newFocus.OnFocused(transform);
      
    }

    // this methond is for stopping the agent when he click on groud or another interactable
    void RemoveFocus()
    {
        if (focus != null)
            focus.Defocused();

        focus = null;

        playerMotor.StopFollowingTarget();
    }
}
