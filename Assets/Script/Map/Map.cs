using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform enterPoint;

    public MapState mapState;
    //public RoomEffect currentEffect;

    public void StartGimmick()
    {
        switch (mapState) 
        {
            case MapState.Normal:
                break;
            case MapState.Oxygen:

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
}

public enum MapState
{
    Normal = 0,
    Oxygen,
    Wind,
    Shop
}
