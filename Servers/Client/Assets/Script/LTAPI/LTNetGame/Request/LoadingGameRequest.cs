using LTNet;

public class StartLoadingRequest : ZombieShootRequestBase
{
	protected override void SetMessageId()
    {
        mMessageId = (uint)NetProtocols.START_LOADING_REQ;
    }
}

public class EndLoadingRequest : ZombieShootRequestBase
{
	protected override void SetMessageId()
    {
        mMessageId = (uint)NetProtocols.END_LOADING_REQ;
    }
}

