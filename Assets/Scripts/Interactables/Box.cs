using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Item
{
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Interact(GameObject actor)
    {
        Debug.Log("Im Box");
    }

}
