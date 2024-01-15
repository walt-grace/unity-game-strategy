using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {
    Mesh _mesh;
    readonly List<Vector3> _vertices = new();
    readonly List<int> _triangles = new();
    const int XSize = 15;
    const int YSize = 15;

    void Start() {
        // _mesh = new Mesh();
        // GetComponent<MeshFilter>().mesh = _mesh;
        // StartCoroutine(CreateShape());
    }

    void Update() {
        // _mesh.Clear();
        // _mesh.vertices = _vertices.ToArray();
        // _mesh.triangles = _triangles.ToArray();
        // _mesh.RecalculateNormals();
    }

    /**
     *
     */
    IEnumerator CreateShape() {
        for (int x = 0; x <= XSize; x++) {
            for (int y = 0; y <= YSize; y++) {
                float z = Mathf.PerlinNoise(x * 0.3f, y * 0.3f) * 2f;
                _vertices.Add(new Vector3(x, y, z));
            }
        }


        int i = 0;
        for (int x = 0; x < XSize; x++) {
            for (int y = 0; y < YSize; y++) {
                _triangles.Add(i);
                _triangles.Add(i + 1);
                _triangles.Add(i + XSize + 2);
                _triangles.Add(i + XSize + 2);
                _triangles.Add(i + XSize + 1);
                _triangles.Add(i);
                i++;
                yield return new WaitForSeconds(0.1f);
            }
            i++;
        }
    }

    void OnDrawGizmos() {
        foreach (Vector3 vertex in _vertices) {
            Gizmos.DrawSphere(vertex, .1f);
        }
    }
}