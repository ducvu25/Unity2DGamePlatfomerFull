using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CrystalType
{
    move,
    idle
}
public class Crystal_Skill : Skill
{
    [SerializeField] GameObject goPreCrystalSkill;
    [SerializeField] float timeALive;
    [SerializeField] float maxSize;
    [SerializeField] float speedSize;
    [SerializeField] float speedMove;
    GameObject index = null;

    [SerializeField] CrystalType type;
    [SerializeField] float coolDownMultiTime;
    float _coolDownMultiTime = 0;
    [SerializeField] int numberStack;
    [SerializeField] List<GameObject> crystalStack = new List<GameObject>();

    public override bool CanUseSkill()
    {
        if (type == CrystalType.move && crystalStack.Count > 0) return true;
        return base.CanUseSkill();
    }

    public void InitCrystal(Transform p)
    {
        if(type == CrystalType.move)
        {
            if(crystalStack.Count > 0)
            {
                index = Instantiate(crystalStack[crystalStack.Count - 1], p.position, p.rotation);
                index.transform.GetComponent<Crystall_Skill_Controller>().SetValue(timeALive, 
                        maxSize, 
                        speedSize, 
                        FindEnemyTransformRandom(PlayerManager.instance.player.transform, distaceFindEnemy), 
                        speedMove);
                crystalStack.RemoveAt(crystalStack.Count - 1);
            }
        }
        else
        {
            index = Instantiate(goPreCrystalSkill, p.position, p.rotation);
            index.transform.GetComponent<Crystall_Skill_Controller>().SetValue(timeALive, maxSize, speedSize, null, speedMove);
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
        if (type == CrystalType.idle)
        {
            if (index != null)
            {
                PlayerManager.instance.player.transform.position = index.transform.position;
                index.transform.GetComponent<Crystall_Skill_Controller>()?.Destroy();
            }
            else
            {
                cooldownTimer = 0;
                InitCrystal(PlayerManager.instance.player.transform);
            }
        }
        else
        {
            InitCrystal(PlayerManager.instance.player.transform);
        }
        
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if(type == CrystalType.move && crystalStack.Count < numberStack)
        {
            if(crystalStack.Count > 0)
                _coolDownMultiTime -= Time.deltaTime;
            if (_coolDownMultiTime <= 0 || (cooldownTimer <= 0 && crystalStack.Count == 0))
            {
                crystalStack.Add(goPreCrystalSkill);
                _coolDownMultiTime = coolDownMultiTime;
            }
        }
    }
}
