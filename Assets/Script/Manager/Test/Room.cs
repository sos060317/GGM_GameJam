using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public static Dictionary<MapState, RoomEffect> effectDictionary;

    static Room()
    {
        effectDictionary = new Dictionary<MapState, RoomEffect>()
        {
            //{MapState.Normal, new NormalRoomEffect() },
            //{MapState.Oxygen, new OxygenRoomEffect() },
            {MapState.Wind, new WindRoomEffect() }

        };
        
    }

    private RoomEffect _roomEffect;

    public void SetRoomEffect(RoomEffect roomEffect)
    {
        _roomEffect = roomEffect;
    }

    public void UpdateEffect()
    {
        _roomEffect.UpdateRoomEffect();
    }
}
