using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class LockToGrid : MonoBehaviour
{
    public int tileSize = 1;
    public Vector3 tileOffset = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        if(!EditorApplication.isPlaying)
        {
            Vector3 currentPos = transform.position;

            float snappedX = Mathf.Round(currentPos.x / tileSize) * tileSize + tileOffset.x;
            float snappedZ = Mathf.Round(currentPos.z / tileSize) * tileSize + tileOffset.z;
            float snappedY = tileOffset.y;

            Vector3 snappedPositoin = new Vector3(snappedX, snappedY, snappedZ);
            transform.position = snappedPositoin;
        }
    }
}
