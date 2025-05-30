using System;

public class P2PProtocolDefine
{
	public static readonly byte P2P_PROTOCOL = 0x2;
	public static readonly uint START_SHAKING_HAND = 0x00010001;
	public static readonly uint SHAKING_HAND_CONFIRMED = 0x00010002;
	public static readonly uint END_SHAKING_HAND = 0x00010003;

	public static readonly uint TEST_ONLY = 0x00020001;
}