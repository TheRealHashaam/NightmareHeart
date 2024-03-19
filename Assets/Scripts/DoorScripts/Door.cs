using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public LockPick LockPick;
    public List<Rigidbody> Rigidbodies;

    private void Start()
    {
        LockDoor();
    }

    void LockDoor()
    {
        for (int i = 0; i < Rigidbodies.Count; i++)
        {
            Rigidbodies[i].isKinematic = true;
        }
    }

    public void UnlockDoor()
    {
        for (int i = 0; i < Rigidbodies.Count; i++)
        {
            Rigidbodies[i].isKinematic = false;
        }
        FindObjectOfType<GameManager>().UpdateObjective();
    }

    public void Interact()
    {
        LockPick.BeginPicking();
        this.gameObject.SetActive(false);
    }
}
