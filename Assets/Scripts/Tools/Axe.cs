using UnityEngine;

public class Axe : Tool
{
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        HandHoldType = Define.HandHold.Tool;
        ToolType = Define.Tool.Axe;
    }

    public override void Use()
    {
        Debug.Log($"Use {ToolType.ToString()}");
    }
}
