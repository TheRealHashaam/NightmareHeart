using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLockpick : MonoBehaviour , IInteractable
{
    public GameObject LockPick;
    private bool isPlayerInside = false;
    public void Pickup()
    {
        if (LockPick != null)
        {
            Destroy(LockPick);
            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().UpdateObjective();
        }
    }
    public void Interact()
    {
        Pickup();
    }

    
}