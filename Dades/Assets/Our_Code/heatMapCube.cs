using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heatMapCube : MonoBehaviour
{

    public List<Color> cubeColors = new List<Color>();
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

    void UpdateColor()
    {
        if (count > cubeColors.Count) count = cubeColors.Count;
        transform.GetComponent<Renderer>().material.color = cubeColors[count];
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("BBBBBBBBBBBB");

        if (other.CompareTag("DeathPos"))
        {
            count++;
            UpdateColor();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAA");
        if (other.CompareTag("DeathPos"))
        {
            count++;
            UpdateColor();
        }
    }
}
