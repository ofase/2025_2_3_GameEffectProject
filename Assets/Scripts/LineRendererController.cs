using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    [SerializeField] List<LineRenderer> lineRenderes = new List<LineRenderer>();

    public void SetPosition(Transform startPos, Transform endPos)
    {
        if(lineRenderes.Count > 0)
        {
            for (int i = 0; i < lineRenderes.Count; i++)
            {
                if (lineRenderes[i].positionCount >=2)
                {
                    lineRenderes[i].SetPosition(0, startPos.position);
                    lineRenderes[i].SetPosition(1, endPos.position);

                }
            }
        }    
    }
}
