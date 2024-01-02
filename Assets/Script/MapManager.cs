using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ������ ���� ������� ����
public class MapManager : MonoBehaviour
{
    [SerializeField] private int roomNumber; // ������ ���� ����
    [SerializeField] private GameObject startMap; // ���� ��
    [SerializeField] private GameObject[] roomArray; // �������� ���� ��� �� ������
    private List<GameObject> randRoomArray = new List<GameObject>(); // �������� ������ ���� �ʵ�

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
    }
}
