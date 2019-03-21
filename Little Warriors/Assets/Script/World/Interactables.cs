using UnityEngine;

public class Interactables : MonoBehaviour
{
    public float radius = 3f;
    bool isFocus = false;
    Transform player;
    public Transform interanctionTransform;
    bool hasInteracted = false;
    private void OnDrawGizmosSelected()
    {
        if (interanctionTransform == null)
            interanctionTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interanctionTransform.position, radius);
    }

    public virtual void Interact()
    {
        // this method is meant to be overwritten
        Debug.Log("Interacting " + transform.name);
    }
    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interanctionTransform.position);
            if(distance<= radius)
            {
                Interact();
                Debug.Log("Interact");
                hasInteracted = true;
                
            }
        }
    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void Defocused()
    {

        isFocus = false;
        player = null;
        hasInteracted = false;

    }
}
