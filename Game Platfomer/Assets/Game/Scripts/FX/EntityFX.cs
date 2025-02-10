using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [SerializeField] Color[] colorEffectFXs;
    Color current;

    SpriteRenderer sp;
    private void Awake()
    {
        sp = GetComponentInChildren<SpriteRenderer>();
    }
    
    public void EffectFx(int type, float timeDelay = 0, float timeRepeat = 0.3f, float timeAc = 2f)
    {
        StartCoroutine(SetFX(type, timeDelay, timeRepeat, timeAc));
    }
    IEnumerator SetFX(int type, float timeDelay, float timeRepeat, float timeAc)
    {
        yield return new WaitForSeconds(timeDelay);
        int n = Mathf.RoundToInt(timeAc / timeRepeat);
        current = sp.color;
        for(int i=0; i<n; i++)
        {
            sp.color = colorEffectFXs[type*2];
            yield return new WaitForSeconds(timeRepeat / 2);
            sp.color = colorEffectFXs[type*2 + 1];
            yield return new WaitForSeconds(timeRepeat / 2);
        }
        sp.color = current;
    }
}
