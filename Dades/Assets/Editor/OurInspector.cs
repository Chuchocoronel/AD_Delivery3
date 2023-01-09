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
            if (GUILayout.Button("Show cubes"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Cubes>().SpawnCubes();
            }
            if (GUILayout.Button("Remove Data From Unity"))
            {
                Resources.FindObjectsOfTypeAll<Our_Code>()[0].GetComponent<Our_Code>().ClearAllDataFromUnity();
            }
            EditorGUILayout.EndToggleGroup();
        }
    }
}