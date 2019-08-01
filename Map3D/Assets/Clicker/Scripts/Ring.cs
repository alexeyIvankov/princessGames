using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ring : MonoBehaviour
{
    public int r1 { get; set; }
    public int r2 { get; set; }

    public Color Color { get; set; }

    public float secNum = 45;

    public Material mat1;

    public static float circle = 2 * Mathf.PI;
    private List<Vector3> innerCircle = new List<Vector3>();
    private List<Vector3> outerCircle = new List<Vector3>();
    public ClickerManager GameManager { get; set; }


    public void Init(Color color)
    {
        Color = color;
        var triangels1 = new List<int>();
        var vertices = new List<Vector3>();
        innerCircle = new List<Vector3>();
        outerCircle = new List<Vector3>();
        int holes = Random.Range(3, 6);
        int[] positions = new int[holes];
        int[] sizes = new int[holes];
        int step = (int) secNum / holes;
        for (int i = 0; i < holes; ++i)
        {
            sizes[i] = Random.Range(2, 4);
            positions[i] = Random.Range(i * step, (i + 1) * step - 1 - sizes[i]);
        }

        int j = 0;
        for (int i = 0; i < holes * step; ++j)
        {
            for (; i < positions[j]; i++)
            {
                var quad = CreateQuad(i);
                vertices.AddRange(quad.Vertices);
                triangels1.AddRange(quad.Triangels);
                innerCircle.AddRange(quad.innerCircleVertices);
                outerCircle.AddRange(quad.outerCircleVertices);
            }

            for (; i < positions[j] + sizes[j]; i++)
            {
                var quad = CreateQuadMini(i);
                vertices.AddRange(quad.Vertices);
                triangels1.AddRange(quad.Triangels);
                innerCircle.AddRange(quad.innerCircleVertices);
                outerCircle.AddRange(quad.outerCircleVertices);
            }

            for (; i < step * (j + 1); i++)
            {
                var quad = CreateQuad(i);
                vertices.AddRange(quad.Vertices);
                triangels1.AddRange(quad.Triangels);
                innerCircle.AddRange(quad.innerCircleVertices);
                outerCircle.AddRange(quad.outerCircleVertices);
            }
        }

        // Дорисовать хвостик у круга
        for (int i = holes * step; i < secNum; i++)
        {
            var quad = CreateQuad(i);
            vertices.AddRange(quad.Vertices);
            triangels1.AddRange(quad.Triangels);
            innerCircle.AddRange(quad.innerCircleVertices);
            outerCircle.AddRange(quad.outerCircleVertices);
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat1;
        GetComponent<MeshRenderer>().material.color = Color;
        mesh.subMeshCount = 2;
        mesh.triangles = triangels1.ToArray();
    }

    public void AddColider()
    {
        drawCollider(innerCircle);
        drawCollider(outerCircle);
    }

    public void drawCollider(List<Vector3> vertices)
    {
        PolygonCollider2D collider = gameObject.AddComponent<PolygonCollider2D>();
        Vector2[] edgePoints = new Vector2[vertices.Count];

        for (int i = 0; i < vertices.Count; i++)
        {
            edgePoints[i] = vertices[i];
        }

        collider.points = edgePoints;
    }

    class Quad
    {
        public List<Vector3> Vertices { get; set; }
        public List<int> Triangels { get; set; }
        public List<Vector3> innerCircleVertices { get; set; }
        public List<Vector3> outerCircleVertices { get; set; }
    }

    Quad CreateQuad(int section)
    {
        return calculateQuad(section, r1, r2);
    }

    Quad CreateQuadMini(int section)
    {
        float r3 = (r1 + r2) / 2f;
        return calculateQuad(section, r1, r3);
    }

    Quad calculateQuad(int section, float innerRadius, float outerRadius)
    {
        float currentAngle = section / secNum * circle;
        Vector3 firstPoint = GetVector3(innerRadius, currentAngle);
        Vector3 secondPoint = GetVector3(outerRadius, currentAngle);
        float nearAngle = (section + 1) / secNum * circle;
        Vector3 thirdPoint = GetVector3(innerRadius, nearAngle);
        Vector3 fourthPoint = GetVector3(outerRadius, nearAngle);
        var vertices = new List<Vector3>();
        vertices.Add(firstPoint);
        vertices.Add(secondPoint);
        vertices.Add(thirdPoint);
        vertices.Add(fourthPoint);

        var innerCircle = new List<Vector3>();
        var outerCircle = new List<Vector3>();
        innerCircle.Add(firstPoint);
        outerCircle.Add(secondPoint);
        innerCircle.Add(thirdPoint);
        outerCircle.Add(fourthPoint);

        var triangels = new List<int>
            {section * 4 + 2, section * 4 + 1, section * 4 + 0, section * 4 + 1, section * 4 + 2, section * 4 + 3};
        return new Quad
        {
            Vertices = vertices,
            Triangels = triangels,
            innerCircleVertices = innerCircle,
            outerCircleVertices = outerCircle
        };
    }


    public void Delete()
    {
        Destroy(transform.gameObject);
    }

    Vector3 GetVector3(float radius, float angle)
    {
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);
        return new Vector3(x, y, 0);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, GameManager.Velocity));
    }
}