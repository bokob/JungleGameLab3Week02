using System.Collections.Generic;
using UnityEngine;

public class Define
{
    // 물건을 드는 데 사용하는 손 개수
    public enum HandHold
    {
        None,
        OneHand,
        TwoHand
    }

    // 도구
    public enum Tool
    {
        None,
        Axe,        // 도끼
        Pickaxe,    // 곡괭이
    }

    public enum Stack
    {
        None,
        Wood,
        Iron,
        Rail
    }

    // 환경
    public enum Environment
    {
        None,
        Tree,   // 나무
        Rock,   // 돌
    }

    // 씬
    public enum Scene
    {
        None,
        TitleScene,
        InGameScene,
        StoreScene
    }
}