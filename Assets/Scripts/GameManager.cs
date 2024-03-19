using DG.Tweening;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
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
    public GameObject MainMenuEnv;
    int count = 0;
    public StarterAssetsInputs Player;
    public GameObject ObjectiveUIOBJ, TrailCanvas;
    public void Failed()
    {
        FadePanel.DOFade(1, 0.5f);
    }
    
    public void MouseState(bool state)
    {
        Player.SetCursorState(state);
    }    

    private void Start()
    {
        if (PlayerPrefs.GetInt("Again") == 0)
        {
            OpenMainMenu();
        }
        else
        {
            StartGame();
        }
        
    }
    public void QuitGame()
    {
        PlayerPrefs.SetInt("Again", 0);
        FadePanel.DOFade(1, 0.5f);
        StartCoroutine(Quit_Delay());
    }
    IEnumerator Quit_Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Again", 0);
    }
    public void PlayAgain()
    {
        PlayerPrefs.SetInt("Again", 1);
        FadePanel.DOFade(1, 0.5f);
        StartCoroutine(Restart_Delay());
    }

    IEnumerator Restart_Delay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
    void OpenMainMenu()
    {
        FadePanel.DOFade(0, 0.5f);
        MouseState(false);
        Mainmenu.SetActive(true);
        MainMenuEnv.SetActive(true);
    }

    public void StartGame()
    {
        FadePanel.DOFade(1, 0.5f);
        ObjectiveUI();
        MouseState(true);
        StartCoroutine(Start_Delay());
    }

    IEnumerator Start_Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Mainmenu.SetActive(false);
        MainMenuEnv.SetActive(false);
        yield return new WaitForSeconds(1f);
        FadePanel.DOFade(0, 0.5f);
        ObjectiveUIOBJ.SetActive(true);
        TrailCanvas.SetActive(true);
        playerInput.enabled = true;
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
