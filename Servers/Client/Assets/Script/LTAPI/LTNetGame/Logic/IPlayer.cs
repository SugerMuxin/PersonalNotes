using UnityEngine;
using System.Collections;

public abstract class IPlayer :MonoBehaviour{

    [System.Serializable]
    public class BeatArmClass
    {
        public GameObject ModelObj;
        public int Attack;
        public int AttackUpWithLevel;
        public float AttackCD;
        public float AttackCDDownWithLevel;
        public float Force = 30;
        public AudioClip AttackAC;
    }

    public Animator ThisAnimator;
    //public PlayerFire[] PlayerFireScripts;
    public BeatArmClass[] BeatArms;
    [System.NonSerialized]
    public int CurGunI;
    public int CurBeatArmI;
    [System.NonSerialized]
    public int CurGunType;
    [System.NonSerialized]
    //public Player.PlayerStateTypeEnum PlayerStateType;
    public int HP;
    public float HPMultipleUpWithLevel;
    [System.NonSerialized]
    public int MaxHP;
    [System.NonSerialized]
    //public ObscuredInt CurHP;
    public int EXP;
    public float EXPMultipleUpWithLevel;
    [System.NonSerialized]
    public int MaxEXP;
    [System.NonSerialized]
    public int MaxViolent = 120;
    [System.NonSerialized]
    public int CurViolent;
    [System.NonSerialized]
    public int ComboKill;
    [System.NonSerialized]
    public int HighestComboKill;


    //public int Level;
    public Renderer PlayerRenderer;
    public GameObject GunLightObj;
    public GameObject NightLightObj;
    public float MoveSpeed = 4;

    [System.NonSerialized]
    public float CurMoveSpeed;


    [System.NonSerialized]
    public bool IsMove;
    [System.NonSerialized]
    public bool IsAttack;
    //[System.NonSerialized]
    public bool IsReload;
    [System.NonSerialized]
    public bool IsReloadSuccess;
    [System.NonSerialized]
    public bool IsAttackDangerRange;
    [System.NonSerialized]
    public bool IsViolent;
    [System.NonSerialized]
    public bool IsBeat;
    [System.NonSerialized]
    public bool IsBeatTarget;
    [System.NonSerialized]
    public bool IsCelebrate;
    [System.NonSerialized]
    public float CurBeatAttackCD;
    [System.NonSerialized]
    public bool IsTrial;
    [System.NonSerialized]
    public float TrialTime = 10;


    /// <summary>
    /// 枪支试用倒计时
    /// </summary>
    [System.NonSerialized]
    public float TempTrialTime;
    public GameObject ViolentEffectObj;



    public AudioClip HurtAudioClip;
    /// <summary>
    /// 主角被击次数
    /// </summary>
    [System.NonSerialized]
    public int HurtNum;

    [System.NonSerialized]
    public AnimatorStateInfo CurAnimatorStateInfo;


    [System.NonSerialized]
    public bool IsBuffShield;
    [System.NonSerialized]
    public float TempBuffShieldTime;

    public virtual void Attack(Transform tf)
    {

    }
    public virtual void DisAttack()
    {

    }

    public virtual  void SetGunLight()
    {

    }

}
