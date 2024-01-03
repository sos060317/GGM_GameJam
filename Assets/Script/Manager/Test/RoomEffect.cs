using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomEffect : MonoBehaviour
{
    protected Room _ownerRoom;
  

    public void SetOwnerRoom(Room ownerRoom)
    {
        _ownerRoom = ownerRoom;
    }

    //그 외에 룸 효과에 필요한 코드들은 여기다가 
    public abstract void UpdateRoomEffect();
}
