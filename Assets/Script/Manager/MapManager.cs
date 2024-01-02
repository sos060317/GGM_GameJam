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

        randRoomArray.Add(Instantiate(startMap, Vector3.zero, Quaternion.identity));

        while (currentRoomNumber < roomNumber)
        {
            int randomMap = Random.Range(0, roomArray.Length);
            int previousMap = -1;

            if (randomMap == previousMap)
                return;

            currentRoomNumber++;
            randRoomArray.Add(Instantiate(roomArray[randomMap], Vector3.zero, Quaternion.identity));
            randRoomArray[randRoomArray.Count - 1].SetActive(false);
            previousMap = randomMap;
        }

        randRoomArray.Add(Instantiate(bossMap, Vector3.zero, Quaternion.identity));
        randRoomArray[randRoomArray.Count - 1].SetActive(false);
    }

    public void OpenMap()
    {
        if (currentMap > randRoomArray.Count)
            return;

        randRoomArray[currentMap + 1].SetActive(true);
        randRoomArray[currentMap].SetActive(false);
        GameManager.Instance.curPlayer.transform.position
            = randRoomArray[currentMap + 1].GetComponent<Map>().enterPoint.position;
        currentMap++;
    }
}
