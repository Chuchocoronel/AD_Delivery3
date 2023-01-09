using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class heatMapCube : MonoBehaviour
{

    public List<Color> cubeColors = new List<Color>();
    public Bounds bounds;
    public int count = 0;


    // Start is called before the first frame update
    void Start()
    {
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Count()
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
        UpdateColor();
    }

    void UpdateColor()
    {
        if (count > cubeColors.Count-1)
            transform.GetComponent<Renderer>().material.color = cubeColors[cubeColors.Count - 1];
        else
        {
            transform.GetComponent<Renderer>().material.color = cubeColors[count];
        }
    }

    public void CleanCubes()
    {
        transform.GetComponent<Renderer>().material.color = cubeColors[0];
    }

}
