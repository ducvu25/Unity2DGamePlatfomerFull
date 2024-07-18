
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public static PlayerSkillManager instance;

    public Dash_Skill dash_Skill;
    public Clone_Skill clone_Skill;
    public Sword_Skill sword_Skill;
    public Hole_Skill hole_Skill;
    public Crystal_Skill crystal_Skill;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        dash_Skill = GetComponent<Dash_Skill>();
        clone_Skill = GetComponent<Clone_Skill>();
        sword_Skill = GetComponent<Sword_Skill>();
        hole_Skill = GetComponent<Hole_Skill>();
        crystal_Skill = GetComponent<Crystal_Skill>();
    }
}
