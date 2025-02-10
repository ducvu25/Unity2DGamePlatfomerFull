using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class Sword_Skill : Skill
{
    [SerializeField] SwordType type;
    [SerializeField] GameObject goPreSword;

    [Header("\n----- Regular, Bounce, Pierce, Spin----")]
    [SerializeField] Vector2[] launchDir;
    [SerializeField] float[] swordGravity;

    [SerializeField] float freezeTime;

    [SerializeField] int numberOfDots;
    [SerializeField] float spaceBeetwenDots;
    [SerializeField] GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    GameObject[] dots;

    Vector2 finalDir;

    

    protected override void Start()
    {
        InitDots();
    }
    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyUp(KeyCode.Mouse1)) {
            Vector2 offset = AimDirctetion().normalized;
            finalDir = new Vector2(offset.x * launchDir[(int)type].x, offset.y * launchDir[(int)type].y);
        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            UpdateIndexDots();
        }
    }
    public void InitSword(Transform p)
    {
        GameObject clone = Instantiate(goPreSword, p.position, p.rotation);
        clone.transform.parent = null;
        PlayerManager.instance.player.SetSword(clone);
        Sword_Skill_Controller sword_Skill_Controller = clone.GetComponent<Sword_Skill_Controller>();
        sword_Skill_Controller.SetUpSword(finalDir, swordGravity[(int)type], PlayerManager.instance.player, type, freezeTime);
    }

    Vector2 AimDirctetion()
    {
        Vector2 playerP = PlayerManager.instance.player.transform.position;
        Vector2 mouseP = PlayerManager.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return mouseP - playerP;
    }
    public void SetDotsActive(bool _active)
    {
        for(int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_active);
        }
    }
    void InitDots()
    {
        dots = new GameObject[numberOfDots];
        for(int i = 0; i < dots.Length; i++)
        {
            dots[i] = Instantiate(dotPrefab, PlayerManager.instance.player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }
    public void UpdateIndexDots()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].transform.position = IndexDots(i * spaceBeetwenDots);
        }
    }
    Vector2 IndexDots(float t)
    {
        Vector2 offset = AimDirctetion().normalized;
        return (Vector2)PlayerManager.instance.player.transform.position
            + new Vector2(launchDir[(int)type].x * offset.x, launchDir[(int)type].y * offset.y) * t
            + 0.5f * (Physics2D.gravity * swordGravity[(int)type]) * t * t;
    }
}
