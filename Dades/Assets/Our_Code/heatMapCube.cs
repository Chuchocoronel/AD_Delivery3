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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CleanCubes()
    {
        var tempMaterial = new Material(this.GetComponent<Renderer>().sharedMaterial);
        tempMaterial.color = cubeColors[0];
        this.GetComponent<Renderer>().sharedMaterial = tempMaterial;
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

        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        if (count > 0)
        {

            materialPropertyBlock.SetColor("Gradiant", gradient.Evaluate((count*100)/totalCount));
            var tempMaterial = new Material(this.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = materialPropertyBlock.GetColor("Gradiant");
            this.GetComponent<Renderer>().sharedMaterial = tempMaterial;
        }
        else
        {
            var tempMaterial = new Material(this.GetComponent<Renderer>().sharedMaterial);
            tempMaterial.color = cubeColors[0];
            this.GetComponent<Renderer>().sharedMaterial = tempMaterial;

        }
        
    }

    //public void SetColor(List<GameObject> heatMapCubes)
    //{
    //    foreach (GameObject thisCube in heatMapCubes)
    //    {
    //        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
    //        float proximityCount = 0;

            
    //        if (proximityCount > 0)
    //        {
    //            materialPropertyBlock.SetColor("Gradiant", gradient.Evaluate(proximityCount / heatMapCubes.Count));
    //        }
    //        // Set the material color of the instance

    //        var tempMaterial = new Material(thisCube.GetComponent<Renderer>().sharedMaterial);
    //        tempMaterial.color = materialPropertyBlock.GetColor("Gradiant");
    //        thisCube.GetComponent<Renderer>().sharedMaterial = tempMaterial;
    //    }
    //}
}

