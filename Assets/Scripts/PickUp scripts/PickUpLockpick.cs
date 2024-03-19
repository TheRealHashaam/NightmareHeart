using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLockpick : MonoBehaviour , IInteractable
{
    public GameObject LockPick;
    public GameObject DoorTrigger;
    private bool isPlayerInside = false;
    public void Pickup()
    {
        if (LockPick != null)
        {
            DoorTrigger.SetActive(true);
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