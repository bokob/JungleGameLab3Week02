using UnityEngine;

public class Pickaxe : Tool
{
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        HandHoldType = Define.HandHold.OneHand;
        ToolType = Define.Tool.Pickaxe;
    }
}