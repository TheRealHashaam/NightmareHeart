using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject PlayerCamera;
    public NurseAI Nurse;
    public TextMeshProUGUI ObjectiveText;
    public string[] objectives;
    int count = 0;

    private void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        ObjectiveUI();
    }

    public void UpdateObjective()
    {
        count++;
        ObjectiveUI();
    }

    public void ObjectiveUI()
    {
        ObjectiveText.text = objectives[count];
    }

}
