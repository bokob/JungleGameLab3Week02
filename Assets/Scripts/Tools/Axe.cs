using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Axe : Tool
{
    void Start()
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

    Vector3 SnapToGrid(Vector3 position)
    {
        float x = Mathf.Round(position.x / 1f) * 1f;
        float y = Mathf.Round(position.y / 1f) * 1f;
        float z = Mathf.Round(position.z / 1f) * 1f;

        return new Vector3(x,y,z);
    }

    public void Place()
    {
        Vector3 dropPosition = SnapToGrid(transform.position);
    }
}