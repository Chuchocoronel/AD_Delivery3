using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;


namespace Gamekit3D
{
    public class Cubes : MonoBehaviour
    {

        private Our_Code ourCode;
        public List<DeathData> data = new List<DeathData>();
        public List<GameObject> allCubes = new List<GameObject>();

        [SerializeField] private GameObject deathCube;
        [SerializeField] private Transform Heatmap;
        [SerializeField] private GameObject HeatMapCubes;
        private List<heatMapCube> heatMapCubesList;

        // Variables Grid
        public int gridX;
        public int gridY;
        public float gridSpacingOffset = 1f;
        [SerializeField] GameObject HeatmapCubePrefab;
        List<GameObject> cubesOfHeatMapList = new List<GameObject>();

        private bool newData = false;
        private void Start()
        {
            ourCode = GetComponent<Our_Code>();
        }

        public void SpawnCubes(List<DeathData> list)
        {
            
            for(int i = 0; i < list.Count; i++)
            {
                Vector3 pos = new Vector3(list[i].X, list[i].Y, list[i].Z);
                GameObject go = Instantiate(deathCube, pos, this.transform.rotation, Heatmap);
                allCubes.Add(go);
            }


            if (heatMapCubesList.Count>0) heatMapCubesList.Clear();


            for (int i = 0; i < HeatMapCubes.transform.childCount; i++)
            {
                heatMapCubesList.Add(HeatMapCubes.transform.GetChild(i).GetComponent<heatMapCube>());
            }

            foreach (heatMapCube item in heatMapCubesList)
            {
                item.Count();
            }
        }
        public void ClearAllCubes()
        {
            GameObject[] aux = GameObject.FindGameObjectsWithTag("DeathPos");
            for (int i = 0; i < aux.Length; ++i)
            {
                DestroyImmediate(aux[i]);
            }
            allCubes.Clear();

            foreach (heatMapCube item in heatMapCubesList)
            {
                item.CleanCubes();
            }
        }

        public void ShowHeatMap(bool active)
        {
            HeatMapCubes.SetActive(active);
        }

        public void SpawnGrid()
        {
            for (int i = 0; i < cubesOfHeatMapList.Count; ++i)
            {
                DestroyImmediate(cubesOfHeatMapList[i]);
            }
            cubesOfHeatMapList.Clear();

            for (int i = 0; i < gridX; i++)
            {
                for (int j = 0; j < gridY; j++)
                {
                    Vector3 spawnPosition = new Vector3(i * gridSpacingOffset, 0, j * gridSpacingOffset) + HeatMapCubes.transform.position;
                    cubesOfHeatMapList.Add(Instantiate(HeatmapCubePrefab, spawnPosition, Quaternion.identity, HeatMapCubes.transform));
                }
            }
        }
    }
}