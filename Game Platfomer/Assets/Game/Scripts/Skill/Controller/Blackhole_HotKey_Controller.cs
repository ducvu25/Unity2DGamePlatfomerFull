using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blackhole_HotKey_Controller : MonoBehaviour
{
    KeyCode keyCode;
    TextMeshProUGUI txtKeyCode;

    Transform myEnemy;
    Blackhole_Skill_Controller skillController;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(keyCode) && txtKeyCode.color != Color.clear) { 
            skillController.AddEnemyTransform(myEnemy);
            txtKeyCode.color = Color.clear;
            Destroy(gameObject);
        }
    }
    public void SetValue(KeyCode _keyCode, Transform e, Blackhole_Skill_Controller b)
    {
        txtKeyCode = GetComponentInChildren<TextMeshProUGUI>();

        keyCode = _keyCode;
        myEnemy = e;
        skillController = b;
        txtKeyCode.text = keyCode.ToString();
    }
}
