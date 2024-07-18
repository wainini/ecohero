using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractables
{
    [SerializeField] private Material highlightMat;
    private Material defaultMat;

    public GameObject GetGameObject()
    {
        return gameObject;
    }
    public void ToggleHighlight()
    {
        GetComponent<SpriteRenderer>().material = highlightMat;
    }

    public void RemoveHighlight()
    {
        GetComponent<SpriteRenderer>().material = defaultMat;
    }

    public void Interact(GameObject actor)
    {
        GameManager.Instance.EnterSeperateTrashMode();
    }

    private void Awake()
    {
        defaultMat = GetComponent<SpriteRenderer>().material;
    }
}
