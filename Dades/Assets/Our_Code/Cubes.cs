using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gamekit3D
{
    public class Cubes : MonoBehaviour
    {

        private Our_Code ourCode;
        public List<DeathData> data = new List<DeathData>();

        [SerializeField] private GameObject deathCube;
        [SerializeField] private Transform Heatmap;

        private bool newData = false;
        private void Start()
        {
            ourCode = GetComponent<Our_Code>();
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.I))
            {
                SpawnCubes();

                data.Clear();
                data = ourCode.GetDownloadedData();

            }

        }

        public void SpawnCubes()
        {
            foreach (Transform item in Heatmap)
            {
                GameObject.Destroy(item.gameObject);
            }

            foreach (var item in data)
            {

                Vector3 pos = new Vector3(item.X,item.Y,item.Z);

                Instantiate(deathCube, pos,this.transform.rotation,Heatmap);
            }
        }
    }
}