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
using System.ComponentModel;

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
        public int Id;
        public string Type;

        public DeathData(float x, float y, float z, float timer, string damagetype, string damager, string type)
        {
            X = x;
            Y = y;
            Z = z;
            Timer = timer;
            DamageType = damagetype;
            Damager = damager;
            Type = type;
        }
        public DeathData(float x, float y, float z, float timer, string damagetype, string damager, string type, int id)
        {
            X = x;
            Y = y;
            Z = z;
            Timer = timer;
            DamageType = damagetype;
            Damager = damager;
            Type = type;
            Id = id;
        }
    }
    [System.Serializable]
    public class HitData
    {
        public float X;
        public float Y;
        public float Z;
        public float Timer;
        public string Damager;
        public string DamageType;
        public string Type;
        public HitData(float x, float y, float z, float timer, string damagetype, string damager, string type)
        {
            X = x;
            Y = y;
            Z = z;
            Timer = timer;
            DamageType = damagetype;
            Damager = damager;
            Type = type;
        }
    }
    

    public class Our_Code : MonoBehaviour
    {
        private GameObject player;

        // ------------------ LISTS ------------------ 
        private List<DeathData> deathDatas = new List<DeathData>();
        private List<HitData> hitDatas = new List<HitData>();
        public List<DeathData> downloadedDeathData = new List<DeathData>();
        public List<DeathData> downloadedDeathDataDamageTypeAcid = new List<DeathData>();
        public List<DeathData> downloadedDeathDataDamageTypeMonsterMelee = new List<DeathData>();
        public List<DeathData> downloadedDeathDataDamageTypeSpit = new List<DeathData>();
        public List<DeathData> downloadedDeathDataDamagerAcid = new List<DeathData>();
        public List<DeathData> downloadedDeathDataDamagerSpit = new List<DeathData>();
        public List<DeathData> downloadedDeathDataDamagerHit = new List<DeathData>();
        public List<DeathData> downloadedDeathDataDamagerDeath = new List<DeathData>();
        public List<Vector3> downloadedPositionsList = new List<Vector3>();
        [SerializeField] private List<Vector3> playerTrackedPositions = new List<Vector3>();
        // ------------------ LISTS ------------------ 

        // 
        public GameObject trackedPositionsArrowGameObject;


        private string[] downloadedString;
        private string[] downloadedStringDownloadPositions;
        [SerializeField]
        public float raycastLength = 1f;
        public GameObject pathTrackerGameObject;
        public float waitTime = 10f;
        private RaycastHit hitt;
        private bool canSpawnCube = true;
        
        private string[] user;
        private string[] userDownloadPositions;
        private string rawresponse;
        private string rawresponseDownloadPositions;
        public bool drawGizmos = false;
        

        // Start is called before the first frame update
        private void Awake()
        {
            player = GameObject.Find("Ellen_Body");
        }
        public void GetDeathPosition(float x, float y, float z, float timer, string damagetype, string damager, string type)
        {
            deathDatas.Add(new DeathData(x, y, z, timer, damagetype, damager, type));
            StartCoroutine(PlayerRequest(x, y, z, timer, damagetype, damager, type));
        }
        public void GetHitPosition(float x, float y, float z, float timer, string damagetype, string damager, string type)
        {
            hitDatas.Add(new HitData(x, y, z, timer, damagetype, damager, type));
            StartCoroutine(PlayerRequest(x, y, z, timer, damagetype, damager, type));
        }
        private void Update()
        {
            if(canSpawnCube)
            {
                StartCoroutine(Wait());
            }
        }
        public void DownloadAllData()
        {
            StartCoroutine(DownloadPositions());
            StartCoroutine(GetInfo());
        }
        public void RemovePath()
        {
            downloadedPositionsList.Clear();
        }
        public void ClearAllDataFromUnity()
        {
            deathDatas.Clear();
            hitDatas.Clear();
            downloadedDeathData.Clear();
            downloadedDeathDataDamagerAcid.Clear();
            downloadedDeathDataDamagerDeath.Clear();
            downloadedDeathDataDamagerHit.Clear();
            downloadedDeathDataDamagerSpit.Clear();
            downloadedDeathDataDamageTypeAcid.Clear();
            downloadedDeathDataDamageTypeMonsterMelee.Clear();
            downloadedDeathDataDamageTypeSpit.Clear();
            downloadedPositionsList.Clear();            
        }
        IEnumerator Wait()
        {
            canSpawnCube = false;
            yield return new WaitForSeconds(waitTime);
            if(RaycastToGround())
            {
                playerTrackedPositions.Add(hitt.point);
                StartCoroutine(UpPositions(hitt.point.x, hitt.point.y, hitt.point.z));
                Debug.DrawRay(player.transform.position, -player.transform.up * raycastLength, Color.blue, 3f);
            }
            canSpawnCube = true;            
        }
        private void OnDrawGizmos()
        {
            if(drawGizmos)
            {
                for (int i = 1; i < downloadedPositionsList.Count; i++)
                {
                    if (i < downloadedPositionsList.Count)
                    {
                        Gizmos.color = Color.red;
                        Gizmos.DrawLine(downloadedPositionsList[i - 1], downloadedPositionsList[i]);
                    }
                }
            }      
        }
        public void DrawTrackLines(bool b)
        {
            drawGizmos = b;
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
        IEnumerator PlayerRequest(float x, float y, float z, float timer, string damagetype, string damager, string type)
        {
            string uri = "https://citmalumnes.upc.es/~marcrp5/Data.php";

            WWWForm form = new WWWForm();
            form.AddField("x", x.ToString());
            form.AddField("y", y.ToString());
            form.AddField("z", z.ToString());
            form.AddField("timer", timer.ToString());
            form.AddField("damagetype", damagetype);
            form.AddField("damager", damager);
            form.AddField("type", type);

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

        public IEnumerator UpPositions(float x, float y, float z)
        {
            string uri = "https://citmalumnes.upc.es/~marcrp5/UpPositions.php";

            WWWForm form = new WWWForm();
            form.AddField("positionx", x.ToString().Replace(",", "."));
            form.AddField("positiony", y.ToString().Replace(",", "."));
            form.AddField("positionz", z.ToString().Replace(",", "."));

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
                        {
                            downloadedDeathData.Add(new DeathData(x, y, z, timer, downloadedString[4], downloadedString[5], downloadedString[6]));
                            switch (downloadedString[4])
                            {
                                case "Acid":
                                    downloadedDeathDataDamageTypeAcid.Add(new DeathData(x, y, z, timer, downloadedString[4], downloadedString[5], downloadedString[6]));
                                    break;
                                case "MonsterMelee":
                                    downloadedDeathDataDamageTypeMonsterMelee.Add(new DeathData(x, y, z, timer, downloadedString[4], downloadedString[5], downloadedString[6]));
                                    break;
                                case "MonsterSpit":
                                    downloadedDeathDataDamageTypeSpit.Add(new DeathData(x, y, z, timer, downloadedString[4], downloadedString[5], downloadedString[6]));
                                    break;
                                default:
                                    break;
                            }
                            switch (downloadedString[5])
                            {
                                case "Acid":
                                    downloadedDeathDataDamagerAcid.Add(new DeathData(x, y, z, timer, downloadedString[4], downloadedString[5], downloadedString[6]));
                                    break;
                                case "Monster":
                                    downloadedDeathDataDamagerSpit.Add(new DeathData(x, y, z, timer, downloadedString[4], downloadedString[5], downloadedString[6]));
                                    break;
                                default:
                                    break;
                            }
                            switch (downloadedString[6])
                            {
                                case "Death":
                                    downloadedDeathDataDamagerDeath.Add(new DeathData(x, y, z, timer, downloadedString[4], downloadedString[5], downloadedString[6]));
                                    break;
                                case "Hit":
                                    downloadedDeathDataDamagerHit.Add(new DeathData(x, y, z, timer, downloadedString[4], downloadedString[5], downloadedString[6]));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }                    
                }
            }
        }

        IEnumerator DownloadPositions()
        {
            using (UnityWebRequest req = UnityWebRequest.Get("https://citmalumnes.upc.es/~marcrp5/DownloadPositions.php"))
            {
                yield return req.SendWebRequest();
                if (req.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("Error: " + req.error);
                }
                else
                {
                    rawresponseDownloadPositions = req.downloadHandler.text;
                    userDownloadPositions = rawresponseDownloadPositions.Split("*");
                    for (int i = 1; i < userDownloadPositions.Length; i++)
                    {
                        downloadedStringDownloadPositions = userDownloadPositions[i].Split("/");
                        float x;
                        float y;
                        float z;
                        bool f1 = float.TryParse((downloadedStringDownloadPositions[0].Replace(".",",")), out x);
                        bool f2 = float.TryParse((downloadedStringDownloadPositions[1].Replace(".", ",")), out y);
                        bool f3 = float.TryParse((downloadedStringDownloadPositions[2].Replace(".", ",")), out z);

                        if (f1 && f2 && f3)
                        {
                            downloadedPositionsList.Add(new Vector3(x,y,z));
                        }

                    }
                }
            }
        }

        public List<DeathData> GetDownloadedData()
        {
            return downloadedDeathDataDamagerDeath;
        }

        public List<Vector3> GetDownloadedPositions()
        {
            return downloadedPositionsList;
        }        
    }
}