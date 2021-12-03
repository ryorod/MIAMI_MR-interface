using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReverseCollider : MonoBehaviour
{
    // public bool removeExistingColliders = true;

    // void Start()
    // {
    //     CreateInvertedMeshCollider();
    // }

    // public void CreateInvertedMeshCollider()
    // {
    //     if (this.removeExistingColliders)
    //         RemoveExistingColliders();

    //     InvertMesh();

    //     this.gameObject.AddComponent<MeshCollider>();
    // }

    // private void RemoveExistingColliders()
    // {
    //     Collider[] colliders = GetComponents<Collider>();
    //     for (int i = 0; i < colliders.Length; i++)
    //         DestroyImmediate(colliders[i]);
    // }

    // private void InvertMesh()
    // {
    //     Mesh mesh = GetComponent<MeshFilter>().mesh;
    //     mesh.triangles = mesh.triangles.Reverse().ToArray();
    // }

    void Start()
    {
        MeshFilter filter = GetComponent(typeof(MeshFilter)) as MeshFilter;
        if (filter != null)
        {
            Mesh mesh = CopyMesh(filter.mesh);

            Vector3[] normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
                normals[i] = -normals[i];
            mesh.normals = normals;

            for (int m = 0; m < mesh.subMeshCount; m++)
            {
                int[] triangles = mesh.GetTriangles(m);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    int temp = triangles[i + 0];
                    triangles[i + 0] = triangles[i + 1];
                    triangles[i + 1] = temp;
                }
                mesh.SetTriangles(triangles, m);
            }
        }

        this.GetComponent<MeshCollider>().sharedMesh = filter.mesh;
    }

    // Update is called once per frame
    void Update () {

    }

    public Mesh CopyMesh(Mesh mesh)
    {
        var copy = new Mesh();
        foreach (var property in typeof(Mesh).GetProperties())
        {
            if (property.GetSetMethod() != null && property.GetGetMethod() != null)
                {
                property.SetValue(copy, property.GetValue(mesh, null), null);
            }
        }
        return copy;
    }
}
