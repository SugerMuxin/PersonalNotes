namespace LTNet
{
	using System;

	public class P2PMessage : SendOutBehaviour, IMessageData
	{
		public byte mProtocol = 0;
		public byte mVersion = 1;
		public ulong	mSrcUId = 0;
		public uint mSrcCId = 0;
		public uint	mSrcEId = 0;
		public ulong mDestUId = 0;
		public uint mDestCId = 0;
		public uint	mDestEId = 0;
		public uint mCheckCode = 0;
		public uint	mMessageId = 0;

		public byte Protocol
		{
			get
			{
				return mProtocol;
			}
		}

		public P2PMessage ()
		{
		}

		protected override void SetProtocol(){}
		protected override void SetMessageId(){}
		protected override void SetServerInstance()
		{
			//do nothing, p2p message doest not require server instance
		}

		protected override Message CreateMessage ()
		{
			SetProtocol();
			SetMessageId();

			return new Message(this);
		}

		public virtual void Serialize(DataStream writer)
		{
			writer.WriteByte(mProtocol);
			writer.WriteByte(mVersion);
			writer.WriteInt64(mSrcUId);
			writer.WriteInt32(mSrcCId);
			writer.WriteInt32(mSrcEId);
			writer.WriteInt64(mDestUId);
			writer.WriteInt32(mDestCId);
			writer.WriteInt32(mDestEId);
			writer.WriteInt32(mCheckCode);
			writer.WriteInt32(mMessageId);
		}

		public virtual void Deserialize(DataStream reader)
		{
			mProtocol = reader.ReadByte();
			mVersion = reader.ReadByte();
			mSrcUId = reader.ReadInt64();
			mSrcCId = reader.ReadInt32();
			mSrcEId = reader.ReadInt32();
			mDestUId = reader.ReadInt64();
			mDestCId = reader.ReadInt32();
			mDestEId = reader.ReadInt32();
			mCheckCode = reader.ReadInt32();
			mMessageId = reader.ReadInt32();
		}

		public void Send(P2PPlayer self, P2PPlayer dest, SocketConnector connector)
		{
			mSrcUId = self.GetUId();
			mSrcCId = self.GetCId();
			mSrcEId = self.GetEId();

			mDestUId = dest.GetUId();
			mDestCId = dest.GetCId();
			mDestEId = dest.GetEId();

			connector.SendNetMessage(this.CreateMessage());
		}
	}
}