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
        bool myBool1 = true;
        damager dmgr;
        damageType dmgtp;
        bool showAllCubes;
        bool showHeatMap;
        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/Data Inspector")]

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
            GUILayout.Label(" ----------------------------------------    General Settings    ---------------------------------------- \n");
            if (GUILayout.Button("Download All Data"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().DownloadAllData();
            }
            GUILayout.Label("\n\n ----------------------------------------    Cube Settings    ---------------------------------------- \n");
            if (GUILayout.Button("Show Cubes from all Data"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathData);
            }
            dmgr = (damager)EditorGUILayout.EnumPopup("Damager", dmgr);
            if (GUILayout.Button("Show Cubes from Damager " + dmgr.ToString()))
            {
                switch (dmgr)
                {
                    case damager.ACID:
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamagerAcid);
                        break;
                    case damager.MONSTER:
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamagerSpit);
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
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamageTypeAcid);
                        break;
                    case damageType.MONSTER_MELEE:
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamageTypeMonsterMelee);
                        break;
                    case damageType.SPIT:
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ClearAllCubes();
                        Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes(Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().downloadedDeathDataDamageTypeSpit);
                        break;
                    default:
                        break;
                }

            }
            GUILayout.Label("\n\n ----------------------------------------    Track Settings    ---------------------------------------- \n");
            myBool0 = GUILayout.Toggle(myBool0, "Draw Track Lines");            
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().DrawTrackLines(myBool0);

            GUILayout.Label("\n\n ----------------------------------------    HeatMap Settings    ---------------------------------------- \n");
            showHeatMap = GUILayout.Toggle(showHeatMap, "Show Heat Map Cubes");
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().ShowHeatMap(showHeatMap);

            GUILayout.Label("\n\n ----------------------------------------    Remove Settings    ---------------------------------------- \n");
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
            //EditorGUILayout.EndToggleGroup();
        }
    }
}