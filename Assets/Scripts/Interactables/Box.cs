using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Item
{

    public override void Interact(GameObject actor)
    {
        Debug.Log("Im Box");
        base.Interact(actor);
    }

}
