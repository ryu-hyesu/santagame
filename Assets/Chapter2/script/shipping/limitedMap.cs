using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitedMap : MonoBehaviour
{
    public Vector2 limitePoint1;
    public Vector2 limitePoint2;
    private void OnDrawGizmos()
    {
        Vector2 lm3 = new Vector2(limitePoint2.x, limitePoint1.y);
        Vector2 lm4 = new Vector2(limitePoint1.x, limitePoint2.y);
        Vector2 lm1 = limitePoint1;
        Vector2 lm2 = limitePoint2;

        Gizmos.color = Color.red;

        Gizmos.DrawLine(lm1, lm4);
        Gizmos.DrawLine(lm1, lm3);
        Gizmos.DrawLine(lm2, lm3);
        Gizmos.DrawLine(lm2, lm4);
    }
}
