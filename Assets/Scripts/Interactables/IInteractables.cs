using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractables 
{
    public void Interact(GameObject actor);
    public GameObject GetGameObject();
}
