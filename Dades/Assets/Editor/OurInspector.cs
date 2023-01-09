using UnityEngine;
using UnityEditor;

namespace Gamekit3D
{
    public class OurInspector : EditorWindow
    {
        string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        float myFloat = 1.23f;

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/Data Inspector")]

        // Add menu named "My Window" to the Window menu
        
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            OurInspector window = (OurInspector)EditorWindow.GetWindow(typeof(OurInspector));
            window.Show();
        }
        void OnGUI()
        {
            if (GUILayout.Button("Download All Data"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().DownloadAllData();
            }
            if (GUILayout.Button("Generate Path"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().GeneratePath();
            }
            if (GUILayout.Button("Show Cubes from all Data"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathData);
            }
            if (GUILayout.Button("Show Cubes from Damager Acid"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamagerAcid);
            }
            if (GUILayout.Button("Show Cubes from Damager Monster"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamagerSpit);
            }
            if (GUILayout.Button("Show Cubes from DamageType Acid"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamageTypeAcid);
            }
            if (GUILayout.Button("Show Cubes from DamageType MonsterMelee"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamageTypeMonsterMelee);
            }
            if (GUILayout.Button("Show Cubes from DamageType Spit"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamageTypeMonsterMelee);
            }
            if (GUILayout.Button("Remove Path Data"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().RemovePath();
            }
            if (GUILayout.Button("Remove Cube Data"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
            }
            if (GUILayout.Button("Remove Data From Unity"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().ClearAllDataFromUnity();
            }
            myBool = GUILayout.Toggle(myBool, "Heatmap");
            Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ShowHeatMap(myBool);
            

            //EditorGUILayout.EndToggleGroup();
        }
    }
}