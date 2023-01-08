using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Gamekit3D.Message;
using UnityEngine.Serialization;
using System;
using UnityEngine.Networking;
using Cinemachine.Utility;
using Unity.VisualScripting;

namespace Gamekit3D
{
    [System.Serializable]

    public class DeathData
    {
        public enum deathType
        {
            MONSTER_MELEE,
            SPIT,
            ACID,
            DEATH,
            FALL
        }
        public float X;
        public float Y;
        public float Z;
        public float Timer;
        public string Damager;
        public string DamageType;

        public DeathData(float x, float y, float z, float timer, string damagetype, string damager)
        {
            X = x;
            Y = y;
            Z = z;
            Timer = timer;
            DamageType = damagetype;
            Damager = damager;
        }
    }
    public class Our_Code : MonoBehaviour
    {
        private GameObject player;


        public List<DeathData> deathDatas = new List<DeathData>();
        public List<DeathData> hitDatas = new List<DeathData>();
        public List<DeathData> downloadedData = new List<DeathData>();
        
        private string[] downloadedString;
        [SerializeField]
        public float raycastLength = 1f;
        public GameObject pathTrackerGameObject;
        public float waitTime = 10f;
        private RaycastHit hitt;
        private bool canSpawnCube = true;
        [SerializeField] private List<Vector3> playerTrackedPositions = new List<Vector3>();
        [SerializeField] private bool showPlayerPathInGame = false;
        [SerializeField] private TrailRenderer tr;
        private string[] user;
        private string rawresponse;
        

        // Start is called before the first frame update
        private void Awake()
        {
            player = GameObject.Find("Ellen_Body");
            tr = GameObject.Find("Our_TrailPosition").GetComponent<TrailRenderer>();
        }
        public void GetDeathPositionByMonster(float x, float y, float z, float timer, string damagetype, string damager)
        {
            deathDatas.Add(new DeathData(x,y,z,timer, damagetype, damager));
            StartCoroutine(PlayerRequest(x, y, z, timer, damagetype, damager));
        }
        public void GetDeathPosition(float x, float y, float z, float timer, string damagetype, string damager)
        {
            deathDatas.Add(new DeathData(x, y, z, timer, damagetype, damager));
            StartCoroutine(PlayerRequest(x, y, z, timer, damagetype, damager));
        }
        public void GetHitPositionBySpit(float x, float y, float z, float timer, string damagetype, string damager)
        {
            hitDatas.Add(new DeathData(x, y, z, timer, damagetype, damager));
            StartCoroutine(PlayerRequest(x, y, z, timer, damagetype, damager));
        }
        public void GetHitPositionByAcid(float x, float y, float z, float timer, string damagetype, string damager)
        {
            hitDatas.Add(new DeathData(x, y, z, timer, damagetype, damager));
            StartCoroutine(PlayerRequest(x, y, z, timer, damagetype, damager));
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                showPlayerPathInGame = !showPlayerPathInGame;
            }
            if(showPlayerPathInGame)
            {
                tr.startWidth = 0.5f;
                tr.endWidth = 0.5f;
            }
            else
            {
                tr.startWidth = 0f;
                tr.endWidth = 0f;
            }
            if(canSpawnCube)
            {
                StartCoroutine(Wait());
            }
            if(Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(GetInfo());
            }
                
        }
        IEnumerator Wait()
        {
            canSpawnCube = false;
            yield return new WaitForSeconds(waitTime);
            if(RaycastToGround())
            {
                playerTrackedPositions.Add(hitt.point);
                Debug.DrawRay(player.transform.position, -player.transform.up * raycastLength, Color.blue, 3f);
            }
            canSpawnCube = true;            
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if(playerTrackedPositions.Count > 0)
            {
                for (int i = 1; i < playerTrackedPositions.Count; i++)
                {
                    Gizmos.DrawLine(playerTrackedPositions[i - 1], playerTrackedPositions[i]);
                }
            }
            
        }
        private bool RaycastToGround()
        {
            RaycastHit hit;
            if(Physics.Raycast(player.transform.position, -player.transform.up, out hit, raycastLength))
            {
                hitt = hit;                          
                return true;
            }
            else
            {
                return false;
            }
        }
        IEnumerator PlayerRequest(float x, float y, float z, float timer, string damagetype, string damager)
        {
            string uri = "https://citmalumnes.upc.es/~marcrp5/Data.php";

            WWWForm form = new WWWForm();
            form.AddField("x", x.ToString());
            form.AddField("y", y.ToString());
            form.AddField("z", z.ToString());
            form.AddField("timer", timer.ToString());
            form.AddField("damagetype", damagetype);
            form.AddField("damager", damager);

            UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.Success:
                        Debug.Log("Reached Succes!");

                        break;
                }
            }
        }

        IEnumerator GetInfo()
        {
            using(UnityWebRequest req = UnityWebRequest.Get("https://citmalumnes.upc.es/~marcrp5/GetInfo.php"))
            {
                yield return req.SendWebRequest();
                if (req.result == UnityWebRequest.Result.ConnectionError)
                    Debug.Log("Error: " + req.error);
                else
                {
                    rawresponse = req.downloadHandler.text;
                    user = rawresponse.Split("*");
                    for (int i = 1; i < user.Length; i++)
                    {
                        downloadedString = user[i].Split("/");
                        float x;
                        float y;
                        float z;
                        float timer;
                        bool f1 = float.TryParse((downloadedString[0]), out x);
                        bool f2 = float.TryParse((downloadedString[1]), out y);
                        bool f3 = float.TryParse((downloadedString[2]), out z);
                        bool f4 = float.TryParse((downloadedString[3]), out timer);

                        if(f1&&f2&&f3&&f4)
                            downloadedData.Add(new DeathData(x, y, z, timer, downloadedString[4], downloadedString[5]));
                    }                    
                }
            }
        }
    }
}

