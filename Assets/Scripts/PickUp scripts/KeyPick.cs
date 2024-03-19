using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPick : MonoBehaviour, IInteractable
{
    public GameObject keyPick;
    private bool isPlayerInside = false;
    public Rigidbody ExitDoor;
    public void Pickup()
    {
        if (keyPick != null)
        {
            Destroy(keyPick);
            Destroy(this.gameObject);
            FindObjectOfType<GameManager>().UpdateObjective();
            ExitDoor.isKinematic = false;
        }
    }
    public void Interact()
    {
        Pickup();
    }
}

   