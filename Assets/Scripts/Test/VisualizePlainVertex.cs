using UnityEngine;

public class VisualizePlainVertex : MonoBehaviour
{
    public float gizmoSize = 0.1f; // 정점 크기

    private void OnDrawGizmos()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
            return;

        Gizmos.color = Color.red; // 정점 색상
        Mesh mesh = meshFilter.sharedMesh;
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            Gizmos.DrawSphere(worldPos, gizmoSize);
        }
    }
}
