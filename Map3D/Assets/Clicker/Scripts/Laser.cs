using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Material mat;

    public Laser Init()
    {
        var vertices = new List<Vector3>();
        var triangels = new List<int>();

        var quad = CreateLaser();
        vertices.AddRange(quad.Vertices);
        triangels.AddRange(quad.Triangels);

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangels.ToArray();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;
        return this;
    }

    class Laser1
    {
        public List<Vector3> Vertices { get; set; }
        public List<int> Triangels { get; set; }
    }

    public void UpdateColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    Laser1 CreateLaser()
    {
        Vector3 firstPoint = new Vector3(0, 0, 0);
        Vector3 secondPoint = new Vector3(0, 5, 0);
        Vector3 thirdPoint = new Vector3(2, 5, 0);
        Vector3 fourthPoint = new Vector3(2, 0, 0);

        var vertices = new List<Vector3>();
        vertices.Add(firstPoint);
        vertices.Add(secondPoint);
        vertices.Add(thirdPoint);
        vertices.Add(fourthPoint);


        var triangels = new List<int> {0, 1, 2, 0, 2, 3};

        return new Laser1
        {
            Vertices = vertices,
            Triangels = triangels
        };
    }

    // Update is called once per frame
    void Update()
    {
    }
}