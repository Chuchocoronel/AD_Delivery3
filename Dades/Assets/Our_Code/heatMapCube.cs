using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class heatMapCube : MonoBehaviour
{

    public Color defaultColor;
    private Bounds bounds;
    public int count = 0;

    public void CleanCubes()
    {
        transform.GetComponent<Renderer>().sharedMaterial.color = defaultColor;
    }

    public int Count()
    {
        count = 0;
        bounds = GetComponent<Renderer>().bounds;

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents);

        foreach (var item in colliders)
        {
            if (item.gameObject.CompareTag("DeathPos"))
            {
                count++;
            }
        }
        return count;
    }

    public void UpdateColor(Gradient gradient, int totalCount)
    {

        if (count > 0)
        {
            transform.GetComponent<Renderer>().sharedMaterial.color = gradient.Evaluate(((float)count/(float)totalCount));
        }
        else
        {
            transform.GetComponent<Renderer>().sharedMaterial.color = defaultColor;
        }
        
    }

}

