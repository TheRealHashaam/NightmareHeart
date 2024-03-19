using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAI : MonoBehaviour
{
    public GameObject Nurse;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Nurse.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
