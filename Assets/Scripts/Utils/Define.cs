using UnityEngine;

public class Define
{
    // 씬
    public enum Scene
    {
        None,
        TitleScene,
        InGameScene,
        StoreScene
    }

    // 환경
    public enum Environment
    {
        None,
        Tree,   // 나무
        Rock,   // 돌
        River   //  강
    }

    // 자원
    public enum Resource
    {
        None,
        Wood,   // 목재
        Iron,   // 철
        Water   // 물
    }

    // 도구
    public enum Tool
    {
        None,
        Axe,        // 도끼
        Pickaxe,    // 곡괭이
        Pail        // 양동이
    }

    // 열차 칸
    public enum Compartment
    {
        None,
        Engine,
        WaterTank,
    }

    // 플레이어 손에 들 수 있는 것
    public enum HandHold
    {
        None,
        Tool,           // 도구
        Resource,       // 자원
        Compartment     // 열차 부품
    }
}