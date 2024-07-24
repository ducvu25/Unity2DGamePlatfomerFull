using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    Entity entity;
    CharacterStats stats;
    RectTransform rectTransform;

    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInParent<Entity>();
        rectTransform = GetComponent<RectTransform>();
        stats = GetComponentInParent<CharacterStats>();
        slider = GetComponentInChildren<Slider>();

        entity.actionFlip += Flip;
        stats.updateHP += UpdateSliderHealth;

        slider.maxValue = stats.GetMaxHealthValue();
        slider.value = slider.maxValue;
    }
    void UpdateSliderHealth()
    {
        slider.maxValue = stats.GetMaxHealthValue();
        slider.value = stats.currentHealth;
    }
    void Flip() => rectTransform.Rotate(0, 180, 0);
    private void OnDisable()
    {
        entity.actionFlip -= Flip;
    }
}
