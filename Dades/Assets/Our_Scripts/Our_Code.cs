using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Gamekit3D.Message;
using UnityEngine.Serialization;
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
        public Vector3 deathPosition;
        public deathType death;
        public float Time;
        public string Message;

        public DeathData(Vector3 pos, deathType type, float time, string message)
        {
            deathPosition = pos;
            death = type;
            Time = time;
            Message = message;
        }
    }
    public class Our_Code : MonoBehaviour
    {
        private GameObject player;


        public List<DeathData> deathDatas = new List<DeathData>();
        public List<DeathData> hitDatas = new List<DeathData>();
        [SerializeField]
        // Start is called before the first frame update
        private void Awake()
        {
            player = GameObject.Find("Ellen");
        }


        public void GetDeathPositionByMonster(float time, string message)
        {
            deathDatas.Add(new DeathData(player.transform.position, DeathData.deathType.MONSTER_MELEE, time, message));
        }
        public void GetDeathPosition(float time, string message)
        {
            deathDatas.Add(new DeathData(player.transform.position, DeathData.deathType.DEATH, time, message));
        }
        public void GetHitPositionBySpit(float time, string message)
        {
            hitDatas.Add(new DeathData(player.transform.position, DeathData.deathType.MONSTER_MELEE, time, message));
        }
        public void GetHitPositionByAcid(float time, string message)
        {
            hitDatas.Add(new DeathData(player.transform.position, DeathData.deathType.ACID, time, message));
        }
    }

}

