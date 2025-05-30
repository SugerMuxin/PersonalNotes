using LTNet;

public class P2P_StartShakingHand : P2PMessage
{
	protected override void SetProtocol()
	{
		mProtocol = P2PProtocolDefine.P2P_PROTOCOL;
	}

	protected override void SetMessageId()
	{
		mMessageId = P2PProtocolDefine.START_SHAKING_HAND;
	}
}

public class P2P_ShakingHandConfirmed : P2PMessage
{
	protected override void SetProtocol()
	{
		mProtocol = P2PProtocolDefine.P2P_PROTOCOL;
	}
	
	protected override void SetMessageId()
	{
		mMessageId = P2PProtocolDefine.SHAKING_HAND_CONFIRMED;
	}
}

public class P2P_EndShakingHand : P2PMessage
{
	protected override void SetProtocol()
	{
		mProtocol = P2PProtocolDefine.P2P_PROTOCOL;
	}
	
	protected override void SetMessageId()
	{
		mMessageId = P2PProtocolDefine.END_SHAKING_HAND;
	}
}

public class P2P_Test : P2PMessage
{
	public uint mData;

	public P2P_Test(){}
	public P2P_Test(uint data)
	{
		mData = data;
	}

	protected override void SetProtocol()
	{
		mProtocol = P2PProtocolDefine.P2P_PROTOCOL;
	}

	protected override void SetMessageId()
	{
		mMessageId = P2PProtocolDefine.TEST_ONLY;
	}

	public override void Serialize(DataStream writer)
	{
		base.Serialize(writer);
		writer.WriteInt32(mData);
	}
	
	public override void Deserialize(DataStream reader)
	{
		base.Deserialize(reader);
		mData = reader.ReadInt32();
	}
}