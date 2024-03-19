using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEManager : MonoBehaviour
{
    public int currentAttackIndex = -1;
    public KeyType[] Keys;
    public eDefenseType requiredDefenseType;
    public eDefenseType currentDefenseType = eDefenseType.None;
    public bool isAttackSequencePlaying;
    public float HitEffect_Delay = 1f;
    public bool AllowQTE;
    public Health health;
    int count = 0;
    public AllItems ItemsUI;
    public float targetTime = 180f;
    private float currentTime;
    public bool timer;
    public Button QTEBUtton;
    [System.Serializable]
    public struct KeyType
    {
        public eDefenseType defenseType;
    }


    void StartTimer()
    {
        AllowQTE = false;
        currentTime = targetTime;
        timer = true;
        QTEBUtton.interactable = false;
    }

    private void Update()
    {
        if(timer)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                timer = false;
                currentTime = 0;
                count = 0;
                AllowQTE = true;
                QTEBUtton.interactable = true;
            }
        }

        if(Input.GetKey(KeyCode.H))
        {
            QTECheck();
        }

        if(isAttackSequencePlaying)
        {
            if(Input.GetKeyUp(KeyCode.W))
            {
                isAttackSequencePlaying = false;
                currentDefenseType = eDefenseType.W;
                CheckQTE();
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                isAttackSequencePlaying = false;
                currentDefenseType = eDefenseType.A;
                CheckQTE();
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                isAttackSequencePlaying = false;
                currentDefenseType = eDefenseType.S;
                CheckQTE();
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                isAttackSequencePlaying = false;
                currentDefenseType = eDefenseType.D;
                CheckQTE();
            }
        }


    }

    public void QTECheck()
    {
        if(AllowQTE)
        {
            AllowQTE = false;
            StartQTE();
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.playerInput.enabled = false;
        }
    }

    void StartQTE()
    {
        if (isAttackSequencePlaying)
            return;

        isAttackSequencePlaying = true;
        requiredDefenseType = eDefenseType.None;
        currentAttackIndex = Random.Range(0, Keys.Length);
        requiredDefenseType = Keys[currentAttackIndex].defenseType;
        Debug.LogError("requiredDefenseType" + requiredDefenseType);
        ShowUI();
    }
    public void ShowUI()
    {
        //InputManager.instance.allowswipe = true;
        ItemsUI.arrowDatas[currentAttackIndex].ResetFill();
        ItemsUI.arrowDatas[currentAttackIndex].gameObject.SetActive(true);
        ItemsUI.arrowDatas[currentAttackIndex].Fill(HitEffect_Delay);
        StartCoroutine(EndAttack_Coroutine());

    }

    public void CheckQTE()
    {
        //if (!isAttackSequencePlaying)
        //    return;

        if (currentDefenseType != requiredDefenseType)
        {
            Debug.Log("Fail");
        }
        else
        {
            health.AddHealth(10);
        }
        count++;
        EndAttack();
    }
    public void EndAttack()
    {

        //if (!isAttackSequencePlaying)
        //    return;

        isAttackSequencePlaying = false;
        requiredDefenseType = eDefenseType.None;
        ItemsUI.arrowDatas[currentAttackIndex].gameObject.SetActive(false);
        if (count<3)
        {
            StartQTE();
        }
        else
        {
            StartTimer();
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.playerInput.enabled = true;
        }
        
        
    }
    IEnumerator EndAttack_Coroutine()
    {
        yield return new WaitForSeconds(HitEffect_Delay);
        CheckQTE();
    }

}
public enum eDefenseType
{
    None, W, A, S, D
}
