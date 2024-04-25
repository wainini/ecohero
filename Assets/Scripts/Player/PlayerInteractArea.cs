using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    [Header("Interact Detection")]
    [SerializeField] private Transform detectPoint;
    [SerializeField] private float areaSize = 2f;
    [SerializeField] private bool drawGizmos = true;
    [SerializeField] private bool showArea = true;
    [SerializeField] private bool showNearest = true;

    private IInteractables nearestInteractable;
    private IInteractables NearestInteractable
    {
        get
        {
            return nearestInteractable;
        }
        set
        {
            if(nearestInteractable == null)
            {
                nearestInteractable = value;
            }
            else if(nearestInteractable != value)
            {
                nearestInteractable.RemoveHighlight();
                nearestInteractable = value;
            }
            if(value != null)
            {
                value.ToggleHighlight();
            }
        }
    }

    private void SearchInteractables()
    {
        float nearestInteractableDistance = float.MaxValue;

        Collider2D[] result = Physics2D.OverlapCircleAll(detectPoint.position, areaSize);
        foreach (Collider2D c in result)
        {
            float distance = Vector2.Distance(c.transform.position, detectPoint.position);

            if (c.TryGetComponent(out IInteractables interactable) && distance < nearestInteractableDistance)
            {
                NearestInteractable = interactable;
                interactable.ToggleHighlight();
                nearestInteractableDistance = distance;
            }
        }

        //There's no interactables in area
        if(nearestInteractableDistance == float.MaxValue)
        {
            NearestInteractable = null;
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        if(showArea)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(detectPoint.position, areaSize);
        }
        if(showNearest && NearestInteractable != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(detectPoint.position, NearestInteractable.GetGameObject().transform.position);
        }
    }
    #endif
}
