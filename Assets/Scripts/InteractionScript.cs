using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance;
    public TextMeshProUGUI textMeshProUGUI;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            Debug.LogError(hit.collider.gameObject.name);
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                textMeshProUGUI.text = "Press E to interact";
                if (Input.GetKey(KeyCode.E))
                {
                    interactable.Interact();
                }
            }
            else
            {
                textMeshProUGUI.text = "";
            }
        }
        else
        {
            textMeshProUGUI.text = "";
        }
    }
    private void OnDisable()
    {
        textMeshProUGUI.text = "";
    }
}
