using UnityEngine;
using System.Collections;
using LTNet;

public class EnterGameRequest : ZombieShootRequestBase
{
    ulong mUserId;

    public EnterGameRequest(ulong id)
    {
        mUserId = id;
    }

    protected override void SetMessageId()
    {
        mMessageId = (uint)NetProtocols.ENTER_GAME_REQ;
    }
   
    public override void Serialize(DataStream writer)
    {
		base.Serialize(writer);
        writer.WriteInt64(mUserId);
    }
}