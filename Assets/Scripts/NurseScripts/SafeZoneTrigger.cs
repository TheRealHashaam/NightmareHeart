using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneTrigger : MonoBehaviour
{
    GameManager _gamemanager;
    private void Awake()
    {
        _gamemanager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gamemanager.Nurse.playerInSafeZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gamemanager.Nurse.playerInSafeZone = false;
        }
    }

}
