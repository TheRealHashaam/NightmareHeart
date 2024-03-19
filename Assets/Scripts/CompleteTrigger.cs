using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteTrigger : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        this.gameObject.SetActive(false);
        FindObjectOfType<GameManager>().Complete();
    }
}
