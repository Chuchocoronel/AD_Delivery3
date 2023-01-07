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
        public float Timet;
        public string Damager;
        public string DamageType;

        public DeathData(float x, float y, float z, float timer, string damagetype, string damager)
        {
            X = x;
            Y = y;
            Z = z;
            Timet = timer;
            DamageType = damagetype;
            Damager = damager;
        }
    }
    public class Our_Code : MonoBehaviour
    {
        private GameObject player;


        public List<DeathData> deathDatas = new List<DeathData>();
        public List<DeathData> hitDatas = new List<DeathData>();
        [SerializeField]
        public float raycastLength;
        // Start is called before the first frame update
        private void Awake()
        {
            player = GameObject.Find("Ellen");
            raycastLength = 10f;
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
            
        }
        private void RaycastToGround()
        {
            RaycastHit hit;
            //if(Physics.Raycast(player.transform.position, ))
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
    }
}

