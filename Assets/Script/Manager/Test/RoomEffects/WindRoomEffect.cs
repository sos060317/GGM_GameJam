using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRoomEffect : RoomEffect
{
    private float _coolTime = 1f;
    private float _lastWindTime;

    public override void UpdateRoomEffect()
    {
        if(_lastWindTime + _coolTime < Time.time)
        {
            _lastWindTime = Time.time;
            Debug.Log("바람이 붑니다.");
            
        }
    }
}
