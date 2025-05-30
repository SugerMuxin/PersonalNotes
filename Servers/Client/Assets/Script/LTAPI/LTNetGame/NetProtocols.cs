using UnityEngine;
using System.Collections;

public class NetProtocols {

	public static readonly uint SERVER_INSTANCE = 0x0501;
	public static readonly byte MESSAGE_PROTOCOL = 0x1;
	public static readonly byte P2P_MESSAGE_PROTOCOL = 0x2;

    public static readonly uint GAME_NETLOGIC_REQ = 1111;
    public static readonly uint GAME_NETLOGIC_RESP = 1111;

    public const uint HANDLE_PROTO_ENTER_IN = 0x00000001;//登录

    public const ushort NTC_DTCD_ENTER_IN_TEST = 999;

    public static readonly uint LOGIN_GAME_REQ = 0x2;
    public static readonly uint LOGIN_GAME_RESP = 0x3;

	public static readonly uint ENTER_GAME_REQ = 0x00010001;   
	public static readonly uint ENTER_GAME_RESP = 0x00010002;

	public static readonly uint UPDATE_ROOM_LIST_REQ = 0x00020001;
	public static readonly uint UPDATE_ROOM_LIST_RESP = 0x00020002;

	public static readonly uint ENTER_ROOM_REQ = 0x00020003;
	public static readonly uint ENTER_ROOM_RESP = 0x00020004;
	public static readonly uint LEAVE_ROOM_REQ = 0x00020005;
	public static readonly uint LEAVE_ROOM_RESP = 0x00020006;

	public static readonly uint READY_REQ = 0x00040001;
	public static readonly uint READY_RESP = 0x00040002;

	public static readonly uint START_LOADING_REQ=0x00040003;//暂时没有用到
	public static readonly uint START_LOADING_RESP=0x00040004;    

	public static readonly uint END_LOADING_REQ=0x00040005;
	public static readonly uint END_LOADING_RESP=0x00040006;

    public static readonly uint TIME_REQ = 0x00040009;
    public static readonly uint TIME_RESP = 0x00040010;

    public static readonly uint MOVE_REQ = 0x00040007;
    public static readonly uint MOVE_RESP = 0x00040008;

}
