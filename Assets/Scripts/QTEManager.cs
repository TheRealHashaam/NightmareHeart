using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    public int currentAttackIndex = -1;
    public KeyType[] Keys;
    public eDefenseType requiredDefenseType;
    public eDefenseType currentDefenseType = eDefenseType.None;
    public bool isAttackSequencePlaying;
    public float HitEffect_Delay = 1f;
    [System.Serializable]
    public struct KeyType
    {
        public eDefenseType defenseType;
    }

    private void Start()
    {
        StartQTE();
    }

    void StartQTE()
    {
        if (isAttackSequencePlaying)
            return;

        isAttackSequencePlaying = true;
        requiredDefenseType = eDefenseType.None;
        currentAttackIndex = Random.Range(0, Keys.Length);
        requiredDefenseType = Keys[currentAttackIndex].defenseType;
        ShowUI();
    }
    public void ShowUI()
    {
        //InputManager.instance.allowswipe = true;
        //ArrowUIData.arrowDatas[enemy.currentAttackIndex].ResetFill();
        //ArrowUIData.arrowDatas[enemy.currentAttackIndex].gameObject.SetActive(true);
        //ArrowUIData.arrowDatas[enemy.currentAttackIndex].Fill(slowMoTransitionDuration);
        StartCoroutine("EndAttack_Coroutine");

    }

    public void CheckQTE()
    {
        if (!isAttackSequencePlaying)
            return;

        if (currentDefenseType != requiredDefenseType)
        {
            Debug.Log("Fail");
        }
        else
        {
            Debug.Log("Nikal");
        }

        EndAttack();
    }
    public void EndAttack()
    {

        if (!isAttackSequencePlaying)
            return;

        isAttackSequencePlaying = false;
        requiredDefenseType = eDefenseType.None;

        //ArrowUIData.arrowDatas[enemy.currentAttackIndex].gameObject.SetActive(false);
    }
    IEnumerator EndAttack_Coroutine()
    {
        yield return new WaitForSeconds(HitEffect_Delay);
        CheckQTE();
        EndAttack();
    }

}
public enum eDefenseType
{
    None, W, A, S, D
}
