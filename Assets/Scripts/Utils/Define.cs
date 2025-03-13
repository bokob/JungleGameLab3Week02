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

    // 환경(자원)
    public enum Environment
    {
        None,
        Tree,
        Rock,
        River
    }

    // 도구
    public enum Tool
    {
        Axe,        // 도끼
        Pickaxe,    // 곡괭이
        Pail        // 양동이
    }
}