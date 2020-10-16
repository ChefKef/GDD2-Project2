using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    int width;
    int height;
    int cellSize;
    int[] indices;
    Vector3[] vertices;
    Mesh gridMesh;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buildIndices(Vector2 startingPoint, int width, int height, int cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridMesh = new Mesh();
        gridMesh.subMeshCount = 1;

        vertices = new Vector3[width * height];

        indices = new int[vertices.Length];
        for(int i = 0; i < vertices.Length; i++)
        {
            indices[i] = i;
        }

        int count = 0;
        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                Vector3 location = new Vector3((int)startingPoint.x + (cellSize * j), (int)startingPoint.y + i, 0.0f);
                vertices[count] = location;
                count++;
            }
        }

        gridMesh.vertices = vertices;

        /*int offs = 0;
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = i;
        }*/
    }

    public void DisplayGrid()
    {
        gridMesh.SetIndices(indices, 0, width * height, MeshTopology.Lines, 0, false, 0);
        this.gameObject.GetComponent<MeshFilter>().mesh = gridMesh;
        Debug.Log(gridMesh);
    }

    public void HideGrid()
    {
        Mesh mr = this.gameObject.GetComponent<Mesh>();
        Destroy(mr);
    }
}
