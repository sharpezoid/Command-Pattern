using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDrawer : MonoBehaviour
{
    public Material mat;

    private void OnPostRender()
    {
        DrawGrid();
    }

    void DrawGrid()
    {
        GL.Begin(GL.LINES);

        mat.SetPass(0);

        GL.Color(Color.red);

        for (int x = 0; x < Level.Instance.Size.x; x++)
        {
            GL.Vertex(Vector3.right * x);
            GL.Vertex(Vector3.right * x + Level.Instance.Size.y * Vector3.forward);
        }
        for (int y = 0; y < Level.Instance.Size.y; y++)
        {
            GL.Vertex(Vector3.forward * y);
            GL.Vertex(Vector3.forward * y + Level.Instance.Size.x * Vector3.right);
        }

        GL.Vertex(Vector3.right * Level.Instance.Size.x);
        GL.Vertex(Vector3.right * Level.Instance.Size.x + Level.Instance.Size.y * Vector3.forward);

        GL.Vertex(Vector3.forward * Level.Instance.Size.y);
        GL.Vertex(Vector3.forward * Level.Instance.Size.y + Level.Instance.Size.x * Vector3.right);

        GL.End();
    }
}
