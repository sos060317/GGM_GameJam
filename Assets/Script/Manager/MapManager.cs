using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ������ ���� ������� ����
public class MapManager : MonoBehaviour
{
    [SerializeField] private int roomNumber; // ������ ���� ����
    [SerializeField] private GameObject startMap; // ���� ��
    [SerializeField] private GameObject bossMap; // ���� ��
    [SerializeField] private GameObject[] roomArray; // �������� ���� ��� �� ������
    private List<GameObject> randRoomArray = new List<GameObject>(); // �������� ������ ���� �ʵ�
    private int currentMap = 0; // ���� �������� ��

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
