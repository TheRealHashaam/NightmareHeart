using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ItemData : MonoBehaviour
{
    public Image ArrowFill;
    public void Fill(float fill)
    {
        ArrowFill.DOFillAmount(1, fill);
    }

    public void ResetFill()
    {
        ArrowFill.fillAmount = 0;
    }
}
