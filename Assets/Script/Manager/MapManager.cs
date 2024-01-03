using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 랜덤한 룸을 순서대로 나열
public class MapManager : MonoBehaviour
{
    [SerializeField] private int roomNumber; // 생성할 방의 개수
    [SerializeField] private GameObject startMap; // 시작 맵
    [SerializeField] private GameObject bossMap; // 보스 맵
    [SerializeField] private GameObject[] roomArray; // 랜덤으로 돌릴 모든 맵 프리팹
    private List<GameObject> randRoomArray = new List<GameObject>(); // 랜덤으로 돌려서 나온 맵들
    private int currentMap = 0; // 현재 진행중인 맵

    private static MapManager instance = null;

    public static MapManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        RoomNumberInitialization();
    }

    private void RoomNumberInitialization()
    {
        int currentRoomNumber = 0;
        int previousMap = -1;

        randRoomArray.Add(Instantiate(startMap, Vector3.zero, Quaternion.identity));
        randRoomArray[randRoomArray.Count - 1].GetComponent<Map>().mapState = MapState.Normal;

        while (currentRoomNumber < roomNumber)
        {
            int randomMap = UnityEngine.Random.Range(0, roomArray.Length);
            

            if (randomMap == previousMap)
                continue;
                
            currentRoomNumber++;
            randRoomArray.Add(Instantiate(roomArray[randomMap], Vector3.zero, Quaternion.identity));
            randRoomArray[randRoomArray.Count - 1].GetComponent<Map>().mapState
                 = (MapState)(UnityEngine.Random.Range(0, Enum.GetNames(typeof(MapState)).Length));
            randRoomArray[randRoomArray.Count - 1].SetActive(false);
            previousMap = randomMap;
        }

        randRoomArray.Add(Instantiate(bossMap, Vector3.zero, Quaternion.identity));
        randRoomArray[randRoomArray.Count - 1].SetActive(false);
        randRoomArray[randRoomArray.Count - 1].GetComponent<Map>().mapState = MapState.Normal;
    }

    public void OpenMap()
    {
        if (currentMap > randRoomArray.Count)
            return;

        Debug.Log(randRoomArray[currentMap].GetComponent<Map>().mapState);
        randRoomArray[currentMap + 1].SetActive(true);
        randRoomArray[currentMap].SetActive(false);
        GameManager.Instance.curPlayer.transform.position
            = randRoomArray[currentMap + 1].GetComponent<Map>().enterPoint.position;
        currentMap++;
    }
}
