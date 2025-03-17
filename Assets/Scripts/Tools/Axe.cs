using UnityEngine;

public class Axe : Tool
{
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        HandHoldType = Define.HandHold.OneHand;
        ToolType = Define.Tool.Axe;
        toolSign = GetComponentInChildren<Canvas>().gameObject;
    }
}