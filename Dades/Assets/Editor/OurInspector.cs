using UnityEngine;
using UnityEditor;
enum damager
{
    ACID,
    MONSTER
}
enum damageType
{
    ACID,
    MONSTER_MELEE,
    SPIT
}
namespace Gamekit3D
{
    
    public class OurInspector : EditorWindow
    {
        bool myBool0 = true;
        damager dmgr;
        damageType dmgtp;
        bool showHeatMap;
        Gradient gradient = new Gradient();
        Vector2 scroll;
        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/Data Inspector")]
        // Add menu named "My Window" to the Window menu
        //private void OnEnable()
        //{
        //    GradientColorKey[] colorKeys = new GradientColorKey[3];
            
        //    colorKeys[0].color = Color.green;
        //    colorKeys[0].time = 0.0f;
        //    colorKeys[1].color = Color.yellow;
        //    colorKeys[1].time = 0.5f;
        //    colorKeys[2].color = Color.red;
        //    colorKeys[2].time = 1.0f;
        //    gradient.SetKeys(colorKeys, gradient.alphaKeys);
        //}

        // Add menu named "My Window" to the Window menu
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            OurInspector window = (OurInspector)EditorWindow.GetWindow(typeof(OurInspector));
            window.Show();

            // Spawning Grid, Not necesary but we let it here
            //Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnGrid();
        }
        void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            scroll = EditorGUILayout.BeginScrollView(scroll);
            GUILayout.Label("\n-------------    General Settings    ------------- ");
            if (GUILayout.Button("Download All Data"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().DownloadAllData();
            }
            GUILayout.Label("\n-------------    Cube Settings    ------------- ");
            if (GUILayout.Button("Show Cubes from all Data"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathData, gradient);
            }
            dmgr = (damager)EditorGUILayout.EnumPopup("Damager", dmgr);
            if (GUILayout.Button("Show Cubes from Damager " + dmgr.ToString()))
            {
                switch (dmgr)
                {
                    case damager.ACID:
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamagerAcid, gradient);
                        break;
                    case damager.MONSTER:
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamagerSpit, gradient);
                        break;
                    default:
                        break;
                }
                
            }
            dmgtp = (damageType)EditorGUILayout.EnumPopup("Damage Type", dmgtp);
            if (GUILayout.Button("Show Cubes from Damage Type " + dmgtp.ToString()))
            {
                switch (dmgtp)
                {
                    case damageType.ACID:
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamageTypeAcid, gradient);
                        break;
                    case damageType.MONSTER_MELEE:
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamageTypeMonsterMelee, gradient);
                        break;
                    case damageType.SPIT:
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamageTypeSpit, gradient);
                        break;
                    default:
                        break;
                }

            }
            GUILayout.Label("\n-------------    Track Settings    ------------- ");
            myBool0 = GUILayout.Toggle(myBool0, "Draw Track Lines");            
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().DrawTrackLines(myBool0);

            GUILayout.Label("\n-------------    HeatMap Settings    ------------- ");
            showHeatMap = GUILayout.Toggle(showHeatMap, "Show Heat Map Cubes");
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ShowHeatMap(showHeatMap);

            gradient = EditorGUILayout.GradientField(gradient);

            GUILayout.Label("\n-------------    Remove Settings    ------------- ");
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
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
            }
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();

        }
    }
}