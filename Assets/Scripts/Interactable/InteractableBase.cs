using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    public virtual void Use()
    {
        Debug.Log("Interactable use");
    }
}
