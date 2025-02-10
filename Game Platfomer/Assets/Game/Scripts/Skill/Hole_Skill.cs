
using UnityEngine;

public class Hole_Skill : Skill
{
    [SerializeField] GameObject goHole;
    [SerializeField] float maxSize;

    [SerializeField] float speed = 0;
    [SerializeField] int numberAttackEnemy;
    [SerializeField] float delayAttack;
    [SerializeField] float distanceAttack;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
    }
    public void InitHole(Vector3 p)
    {
        GameObject go = Instantiate(goHole, p, Quaternion.identity);
        go.transform.GetComponent<Blackhole_Skill_Controller>()?.SetValue(maxSize, speed, numberAttackEnemy, delayAttack, distanceAttack);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

}
