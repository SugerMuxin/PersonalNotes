using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerVOManager : MonoBehaviour
{
	[System.NonSerialized]
    public List<PlayerVO> mPlayerList = new List<PlayerVO>();
	public static PlayerVOManager Self;

	void Awake()
	{
		Self = this;
	}

    public PlayerVO GetPlayerById(long id)
    {
        foreach(PlayerVO vo in mPlayerList)
		{
			if (vo.mUserId == id)
				return vo;
		}

        return null;
    }
}
