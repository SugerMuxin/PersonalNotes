using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LTNet;


public class XPlayer
{
	public string mName;
	Vector3 mVelocity;
	Vector3 mRealVelocity;
	Vector3 mTargetPos;                                                 
	public ulong mLastTimeOnPos;
    public ulong mLastStartMoveTime;
	protected PlayerCollisionCheck mCollisionCheck;
	protected IPlayer mPlayer;
	protected GameObject mPlayerObject;

    public XPlayer()
    {
        mLastStartMoveTime = 0xFFFFFFFFFFFFFFFF;
    }

    public GameObject PlayerObject
    {
        get
		{
			return mPlayerObject;
		}

        set
		{
			mPlayerObject = value;
			mCollisionCheck = mPlayerObject.transform.Find("CollisionChecker").GetComponent<PlayerCollisionCheck>();
			mPlayer = mPlayerObject.GetComponent<RemotePlayer>();
		}
    }

    public void ReCalculate(Vector3 targetPos, ulong timeOnPos, Vector3 realVelocity, ulong moveStartTime)
    {
		//DebugUtil.Log("CurrentServerTime - timeOnPos: " + (TimeManager.Self.GetCurrentServerTime() - timeOnPos));

        mRealVelocity = realVelocity;
		mTargetPos = targetPos;

		//moveStartTime == 0 indicates that the player is not moving at the moment
        if (moveStartTime == 0)
        {
            mVelocity = Vector3.zero;
            mLastTimeOnPos = timeOnPos;
            return;
        }

        if (mLastStartMoveTime != moveStartTime)
        {
            mLastTimeOnPos = timeOnPos;
            mLastStartMoveTime = moveStartTime;
        }

        ulong dt = timeOnPos - mLastTimeOnPos;//millisecond
        Debug.Log("dt :" + dt);

		//when dt is a very small number it also should be considered as 0
        if (dt < 100)
            return;

        mLastTimeOnPos = timeOnPos;
        //pos = pos + realVelocity * TimeManager.Self.GetDelayToServer();
		mVelocity = (targetPos - PlayerObject.transform.position) / (dt * 0.001f);
    }

	public void ForceSync()
	{
		//mPlayerObject.transform.position = mTargetPos;

		if (mRealVelocity != Vector3.zero && !mPlayer.IsAttack)
			mPlayerObject.transform.rotation = Quaternion.LookRotation(mRealVelocity);
	}

    public void Move()
    {
        if (mVelocity != Vector3.zero || Vector3.Distance(mTargetPos, PlayerObject.transform.position) > 0.1f)
		{
            mPlayer.IsMove = true;

            //if (!mCollisionCheck.mHitObstacle)
            {
                CharacterController controller = PlayerObject.GetComponent<CharacterController>();
				//controller.Move(mVelocity * Time.deltaTime);

				//limit the movement in case the player flushes
				Vector3 movement = mVelocity == Vector3.zero ? (mTargetPos - PlayerObject.transform.position).normalized * mPlayer.MoveSpeed * Time.deltaTime
					: mVelocity * Time.deltaTime;
				movement = Vector3.ClampMagnitude(movement, Vector3.Distance(mTargetPos, PlayerObject.transform.position));
				controller.Move (movement);
            }
        }
        else
        {
            mPlayer.IsMove = false;
        }

        //change rotation
        if (mRealVelocity != Vector3.zero && !mPlayer.IsAttack)
        {
            //mPlayerObject.transform.rotation = Quaternion.Slerp(mPlayerObject.transform.rotation, Quaternion.LookRotation(mRealVelocity), DataManager.DeltaTime * 10);
        }

    }
}

public class PlayerManager : MonoBehaviour {
    /*
    public Dictionary<long, XPlayer> mPlayersDic = new Dictionary<long, XPlayer>();

    public GameObject prefab;

    public Vector3[] mStartPosition;

    public void AddPlayer(long id, XPlayer player)
    {
        if(!mPlayersDic.ContainsKey(id))
        {
            mPlayersDic.Add(id, player);
        }
    }

    private void init()
    {
        long selfId = long.Parse(NetworkManager.UserId);

        int i = 0;
        foreach(var playerVO in  PlayerVOManager.Self.mPlayerList)
        {
            if (playerVO.mUserId != selfId)
            {
                GameObject go = (GameObject)GameObject.Instantiate(prefab, mStartPosition[i], transform.localRotation);     
                go.GetComponent<PlayerControl>().enabled = false;
                go.GetComponent<RemotePlayer>().enabled = true;
                go.GetComponent<Player>().enabled = false;
                go.GetComponentInChildren<Attack>().init();
                XPlayer temp_player = new XPlayer();
                temp_player.PlayerObject = go;
                temp_player.mName = playerVO.mUsername;
                go.transform.position = playerVO.mPosition;
                AddPlayer(playerVO.mUserId, temp_player);
            }
            else
            {
                Player.Self.gameObject.transform.position = playerVO.mPosition;
            }
        }
    }

    public void updatePlayerPositionCallback(Message msg)
    {
        PositionResponse data = (PositionResponse)msg.GetMessageData();
        Vector3 postion = new Vector3(data.mPosX/1000.0f,0 ,data.mPosY /1000.0f);
        Vector3 velocity = new Vector3(data.mVelocityX / 1000.0f, 0, data.mVelocityY/ 1000.0f);

		//强制同步转向和位置
		mPlayersDic[(long)data.mId].ForceSync();

		mPlayersDic[(long)data.mId].ReCalculate(postion,
                                                data.mTime,
                                                velocity, 
		                                        data.mMoveStartTime);
    }

	// Use this for initialization
	void Start () {
		NetworkManager.Self.RegisterHandler<PositionResponse>(updatePlayerPositionCallback);
        init();
	}
	
	// Update is called once per frame
	void Update () {
        updatePlayerPosition();
	}

    private void updatePlayerPosition()
    {
        foreach(var player in mPlayersDic)
        {
            player.Value.Move();
        }
    }
    */
}
