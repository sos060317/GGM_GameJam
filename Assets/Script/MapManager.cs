using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���� ������� ����
public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject startMap; // ���� ��
    [SerializeField] private GameObject[] roomArray; // �������� ���� ��� �� ������
    private List<GameObject> randRoomArray = new List<GameObject>(); // �������� ������ ���� �ʵ�

    private void Start()
    {
        randRoomArray.Add(startMap);

        for(int i = 0; i < roomArray.Length; i++) 
        {
            int previousMap;
            int randomMap = Random.Range(0, roomArray.Length);
            randRoomArray.Add(roomArray[randomMap]);
            previousMap = randomMap;
        }
    }


}
