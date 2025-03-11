using UnityEngine;

public class WorldGen : MonoBehaviour
{
    GameObject StartingPoint;
    int RandomStartingBiome;
    int possibility;
    public Material Green;
    public Material Blue;

    void Start()
    {
        StartingPoint = GameObject.Find("StartingPoint");
        RandomStartingBiome = Random.Range(1, 3);
        switch (RandomStartingBiome)
        {
            case 1:
                StartingPoint.tag = "River";
                StartingPoint.GetComponent<Renderer>().material = Blue;
                break;
            case 2:
                StartingPoint.tag = "GrassLand";
                StartingPoint.GetComponent<Renderer>().material = Green;
                break;
        }
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            possibility = Random.Range(1, 100);
            if (gameObject.tag == "GrassLand")
            {
                if (possibility < 70)
                {
                    other.tag = "GrassLand";
                    other.GetComponent<BoxCollider>().isTrigger = true;
                    other.GetComponent<MeshRenderer>().material = Green;
                    if (possibility > 70)
                    {
                        other.tag = "River";
                        other.GetComponent<BoxCollider>().isTrigger = true;
                        other.GetComponent<MeshRenderer>().material = Blue;
                    }
                }
                if (gameObject.tag == "River")
                {
                    if (possibility < 70)
                    {
                        other.tag = "River";
                        other.GetComponent<BoxCollider>().isTrigger = true;
                        other.GetComponent<MeshRenderer>().material = Green;
                    }
                    if (possibility > 70)
                    {
                        other.tag = "GrassLand";
                        other.GetComponent<BoxCollider>().isTrigger = true;
                        other.GetComponent<MeshRenderer>().material = Blue;
                    }
                }
            }
        }
    }
}
