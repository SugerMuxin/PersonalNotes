using LTNet;

public class ReadyGameRequest : ZombieShootRequestBase
{
	protected override void SetMessageId()
	{
		mMessageId = (uint)NetProtocols.READY_REQ;
	}
}
