using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiShowLevelOfStrength : MonoBehaviour
{
    [SerializeField] float timeDelay = 0.5f;
    [SerializeField] float[] valueScale;
    [SerializeField] int lv;
    int k;

    private void Start()
    {
        SetColor(lv);
    }
    public void SetColor(int _lv)
    {
        lv = _lv;
        k = ((lv - 1) % 4) + 1;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = GameValue.Instance.colorsLevelOfStrength[lv];
            transform.GetChild(i).localScale = Vector3.zero;
        }
    }
    void OnEnable()
    {
        ShowEffect();
    }
    void ShowEffect()
    {
        StartCoroutine(ShowUi());
    }
    IEnumerator ShowUi()
    {
        int indexShow = 0;
        while (true)
        {
            yield return new WaitForSecondsRealtime(timeDelay);
            for(int j=0; j<k; j++)
            {
                for (int i = 0; i < valueScale.Length; i++)
                {
                    LeanTween.scale(transform.GetChild((indexShow + transform.childCount * j / k + i) % transform.childCount).gameObject,
                        valueScale[i] * Vector3.one, timeDelay).setIgnoreTimeScale(true) ;
                }
            }

            indexShow = (indexShow + 1)%transform.childCount;
        }
    }
}
