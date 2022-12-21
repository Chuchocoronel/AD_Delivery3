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

    public DeathData(Vector3 pos, deathType type)
    {
        deathPosition = pos;
        death = type;
    }
}
public class Our_Code : MonoBehaviour
{
    private GameObject player;


    public List<DeathData> deathDatas = new List<DeathData>();
    [SerializeField]
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Ellen");
    }
    


    public void GetDeathPositionByMonster()
    {
        deathDatas.Add(new DeathData(player.transform.position,DeathData.deathType.MONSTER));
    }
    public void GetDeathPositionByLava()
    {
        deathDatas.Add(new DeathData(player.transform.position, DeathData.deathType.LAVA));
    }
}

