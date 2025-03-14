using UnityEngine;

public class Pickaxe : Tool
{
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        HandHoldType = Define.HandHold.Tool;
        ToolType = Define.Tool.Pickaxe;
    }

    public override void Use()
    {
        Debug.Log($"Use {ToolType.ToString()}");
    }
}
