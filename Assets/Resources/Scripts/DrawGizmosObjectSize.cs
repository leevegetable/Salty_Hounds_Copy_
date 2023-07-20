using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmosObjectSize : MonoBehaviour
{
    public Color color = new Color(0,0,0,1);

    public void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
