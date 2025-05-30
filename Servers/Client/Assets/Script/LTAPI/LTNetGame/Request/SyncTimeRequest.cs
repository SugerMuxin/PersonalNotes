using LTNet;

public class SyncTimeRequest : ZombieShootRequestBase
{
	public SyncTimeRequest()
	{
	}

	protected override void SetMessageId()
	{
		mMessageId = NetProtocols.TIME_REQ;
	}
}