using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRay : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private Material material;
    private ClickerManager _gameManager;
    private Ray ray = CreateLaser();
    public int currentLvl;


    public CreateRay Init(ClickerManager gameManager)
    {
        _gameManager = gameManager;
        Ray ray = CreateLaser();
        Mesh mesh = new Mesh();
        mesh.vertices = ray.Vertices.ToArray();
        mesh.triangles = ray.Triangels.ToArray();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = material;
        GetComponent<MeshRenderer>().material.color = gameManager.rayColor;
        return this;
    }

    class Ray
    {
        public List<Vector3> Vertices { get; set; }
        public List<int> Triangels { get; set; }
    }


    static Ray CreateLaser()
    {
        Vector3 firstPoint = new Vector3(0, 0, 0);
        Vector3 secondPoint = new Vector3(0, 1.5f, 0);
        Vector3 thirdPoint = new Vector3(0.75f, 1.5f, 0);
        Vector3 fourthPoint = new Vector3(0.75f, 0, 0);

        var vertices = new List<Vector3>();
        vertices.Add(firstPoint);
        vertices.Add(secondPoint);
        vertices.Add(thirdPoint);
        vertices.Add(fourthPoint);

        var triangels = new List<int> {0, 1, 2, 0, 2, 3};

        return new Ray
        {
            Vertices = vertices,
            Triangels = triangels
        };
    }


    void FixedUpdate()
    {
        if (!_rigidbody2) return;
        _rigidbody2.velocity = _velocity;

        Ring myColorRing = GetMyColorRing();
        if (myColorRing)
        {
            var enterRadius = myColorRing.r2 - 0.15f;
            if (Math.Abs(transform.position.y) <= enterRadius)
            {
                Destroy(transform.gameObject);
                _gameManager.DeleteRing(myColorRing);
                _gameManager.UpdateColor();
            }
        }
    }

    private Ring GetMyColorRing()
    {
        Ring myColorRing = null;
        foreach (var ringNum in _gameManager.existingRings)
        {
            if (ringNum.Color != _gameManager.rayColor) continue;
            myColorRing = ringNum;
            break;
        }

        return myColorRing;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Ring>().Color == _gameManager.rayColor)
        {
            Destroy(transform.gameObject);
            _gameManager.Timer.bonusTime -= 2;
        }
    }
}