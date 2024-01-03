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

    //�� �ܿ� �� ȿ���� �ʿ��� �ڵ���� ����ٰ� 
    public abstract void UpdateRoomEffect();
}
