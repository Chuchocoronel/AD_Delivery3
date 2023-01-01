using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeathData
{
    public enum deathType
    {
        MONSTER,
        LAVA,
        FALL
    }
    public Vector3 deathPosition;
    public deathType death;
    public float Time;

    public DeathData(Vector3 pos, deathType type, float time)
    {
        deathPosition = pos;
        death = type;
        Time = time;
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


    public void GetDeathPositionByMonster(float time)
    {
        deathDatas.Add(new DeathData(player.transform.position,DeathData.deathType.MONSTER, time));
    }
    public void GetDeathPositionByLava(float time)
    {
        deathDatas.Add(new DeathData(player.transform.position, DeathData.deathType.LAVA, time));
    }
    public void GetHitPositionByMonster(float time)
    {
        hitDatas.Add(new DeathData(player.transform.position, DeathData.deathType.MONSTER, time));
    }
}

