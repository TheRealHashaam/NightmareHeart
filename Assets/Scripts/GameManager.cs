using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject PlayerCamera;
    public NurseAI Nurse;
    public Image FadePanel;
    public GameObject Mainmenu;
    public TextMeshProUGUI ObjectiveText;
    public string[] objectives;
    int count = 0;
    public void Failed()
    {
        FadePanel.DOFade(1, 0.5f);
    }
    

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

    void ObjectiveUI()
    {
        ObjectiveText.text = objectives[count];
    }

}
