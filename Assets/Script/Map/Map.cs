using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject oxygen;

    public Transform enterPoint;

    public MapState mapState;
    //public RoomEffect currentEffect;

    public GameObject door;

    public void StartGimmick()
    {
        switch (mapState) 
        {
            case MapState.Normal:
                break;
            case MapState.Oxygen:
                oxygen.SetActive(true);
                break;

            case MapState.Wind:

                break;
            
            case MapState.Shop:

                break;
        }
    }

    //public void SetActiveRoom()
    //{
    //    currentEffect = Room.effectDictionary[mapState];
    //    currentEffect.SetOwnerRoom(this);
    //}

    //private void Update()
    //{
    //    currentEffect?.UpdateRoomEffect();
    //}

    public void MapClear()
    {
        door.SetActive(false);
    }
}

public enum MapState
{
    Normal = 0,
    Oxygen,
    Wind,
    Shop
}
