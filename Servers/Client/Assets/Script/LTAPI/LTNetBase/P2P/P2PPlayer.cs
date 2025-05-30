namespace LTNet
{
	public class P2PPlayer
	{
		private ulong mUId;
		private uint mCId;
		private uint mEId;

		public P2PPlayer (ulong uId, uint cId, uint eId)
		{
			mUId = uId;
			mCId = cId;
			mEId = eId;
		}

		public ulong GetUId()
		{
			return mUId;
		}

		public uint GetCId()
		{
			return mCId;
		}

		public uint GetEId()
		{
			return mEId;
		}
	}
}

