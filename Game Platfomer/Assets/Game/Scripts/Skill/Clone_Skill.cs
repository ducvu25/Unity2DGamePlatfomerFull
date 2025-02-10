
using UnityEngine;


public class Clone_Skill : Skill
{
    [SerializeField] GameObject goPreCloneSkill;

    public void InitCloneIdle(Transform p, float time, Vector3 offset,bool face)
    {
        GameObject clone = Instantiate(goPreCloneSkill, p.position + offset, p.rotation);
        clone.GetComponent<Clone_Skill_Controller>().Setvalue(time, FindEnemyTransform(p, distaceFindEnemy), face);
    }
    public void InitCloneMove(Transform p, float time, Vector3 target, bool face, float d)
    {
        GameObject clone = Instantiate(goPreCloneSkill, target, Quaternion.Euler(0, face ? 0 : 180, 0));
        clone.GetComponent<Clone_Skill_Controller>().Setvalue(time, p.position, face, d);
    }
}
