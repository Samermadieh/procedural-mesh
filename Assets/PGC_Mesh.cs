using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGC_Mesh : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 50;
    public int zSize = 50;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        gameObject.AddComponent<MeshFilter>();
        GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        gameObject.AddComponent<MeshRenderer>();
        GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));

        CreateShape();
        UpdateMesh();

        GameObject prim = GameObject.CreatePrimitive(PrimitiveType.Cube);
        prim.transform.localScale = new Vector3(2, 2, 2);
        prim.transform.position = new Vector3(25, 15, 25);
        prim.AddComponent<Rigidbody>();
        prim.AddComponent<BoxCollider>();
    }

    void Update()
    {
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        float amp = 3f;
        float wave = 8f;
        int index = 0;
        for(int z = 0; z < (zSize + 1); z++)
        {
            for(int x = 0; x < (xSize + 1); x++)
            {
                float y = amp * Mathf.Sin((Mathf.PI * (x / wave)) + Time.time);
                vertices[index] = new Vector3(x, y, z);
                index++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vertex = 0;
        int triCount = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[triCount + 0] = vertex + 0;
                triangles[triCount + 1] = vertex + xSize + 1;
                triangles[triCount + 2] = vertex + 1;
                triangles[triCount + 3] = vertex + 1;
                triangles[triCount + 4] = vertex + xSize + 1;
                triangles[triCount + 5] = vertex + xSize + 2;
                vertex++;
                triCount += 6;
            }
            vertex++;
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        MeshCollider collidr = gameObject.GetComponent<MeshCollider>();
        collidr.sharedMesh = mesh;
    }

}
