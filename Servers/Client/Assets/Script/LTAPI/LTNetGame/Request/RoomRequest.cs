using UnityEngine;
using System.Collections;
using LTNet;

public class UpdateRoomListRequest : ZombieShootRequestBase {

	protected override void SetMessageId()
    {
        mMessageId = (uint)NetProtocols.UPDATE_ROOM_LIST_REQ;
    }

}

public class EnterRoomRequest : ZombieShootRequestBase
{
    uint mRoomID;

    public EnterRoomRequest(uint id)
    {
        mRoomID = id;
    }

	protected override void SetMessageId()
    {
        mMessageId = (uint)NetProtocols.ENTER_ROOM_REQ;
    }

    public override void Serialize(DataStream writer)
    {
		base.Serialize(writer);
        writer.WriteInt32(mRoomID);
    }
}

public class LeaveRoomRequest : ZombieShootRequestBase
{
    protected override void SetMessageId()
    {
        mMessageId = (uint)NetProtocols.LEAVE_ROOM_REQ;
    }   
}



