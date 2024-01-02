using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 랜덤한 룸을 순서대로 나열
public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject startMap; // 시작 맵
    [SerializeField] private GameObject[] roomArray; // 랜덤으로 돌릴 모든 맵 프리팹
    private List<GameObject> randRoomArray = new List<GameObject>(); // 랜덤으로 돌려서 나온 맵들

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
