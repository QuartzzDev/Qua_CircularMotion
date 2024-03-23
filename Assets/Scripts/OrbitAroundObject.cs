/*                                                                                                                                                                                                  
                                                                                            *********************
                                                                                            *     QuartzzDev    *
                                                                                            ********************* 
*/

using UnityEngine;

public class OrbitAroundObject : MonoBehaviour
{

    public float radius;
    public Transform targetObject; 
    public int resolution = 100; 
    public LineRenderer lineRendererPrefab; 
    private LineRenderer lineRenderer;



    void Start()
    {
        lineRenderer = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity);
        lineRenderer.positionCount = resolution + 1;
    }

    public void Update()
    {
        radius = GetComponent<OrbitController>().radius;
        DrawOrbit();
    }

    void DrawOrbit()
    {
        if (targetObject == null)
        {
            Debug.LogError("Hedef obje belirtilmedi.");
            return;
        }

        // Ne yaptýðýný bilmiyorsan dokunma
        for (int i = 0; i <= resolution; i++)
        {
            float angle = (float)i / resolution * 360f;

            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            Vector3 orbitPosition = targetObject.position + new Vector3(x, 0f, z);

            lineRenderer.SetPosition(i, orbitPosition);
        }
    }
}
