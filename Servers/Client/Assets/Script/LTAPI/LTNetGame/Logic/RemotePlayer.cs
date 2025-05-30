using UnityEngine;
using System.Collections;

public class RemotePlayer : IPlayer
{
  
    /*
    bool IsAnimatorNormal;

    float lastFireCD;
    float LastMoveSpeed;
    //float CurMoveSpeed;
   

    int curReloadGunI;

    Transform ThisTF;
    Transform AttackTargetTF;

    void Start()
    {     
        CurMoveSpeed = MoveSpeed;
        CurGunI = 0;//DataManager.Self.SaveEquipGunI;
       // lastFireCD = PlayerFireScripts[CurGunI].CurFireCD;
        lastFireCD = PlayerFireScripts[CurGunI].CurFireCD;
        ThisAnimator.SetFloat("FireCD", lastFireCD);
        ThisAnimator.SetInteger("GunType", 0);
        InitialiseData();
        ThisTF = gameObject.transform;
        PlayerFireScripts[CurGunI].gameObject.SetActive(true);
    }
    /// <summary>
    /// 数据初始化
    /// </summary>
    public void InitialiseData()
    {
        PlayerStateType =Player.PlayerStateTypeEnum.Idle;
        PlayerFireScripts[CurGunI].gameObject.SetActive(true);
        TempViolentTime = 0;
        IsBeat = false;
        ThisAnimator.SetBool("IsBeat", false);
        IsCelebrate = false;
        ThisAnimator.SetBool("IsCelebrate", false);
        IsReload = false;
        ThisAnimator.SetBool("IsReload", false);
        ThisAnimator.SetBool("IsDead", false);
    }


    void UpdateCommon()
    {
        if (DataManager.IsGamePause)
        {
            if (!IsAnimatorNormal)
            {
                ThisAnimator.updateMode = AnimatorUpdateMode.Normal;
                IsAnimatorNormal = true;
            }
            return;
        }
        CurAnimatorStateInfo = ThisAnimator.GetCurrentAnimatorStateInfo(1);
        if (lastFireCD != PlayerFireScripts[CurGunI].CurFireCD)
        {
            lastFireCD = PlayerFireScripts[CurGunI].CurFireCD;
            ThisAnimator.SetFloat("FireCD", lastFireCD);
        }
        if (IsMove)
        {
            if (LastMoveSpeed != CurMoveSpeed)
            {
                LastMoveSpeed = CurMoveSpeed;
                ThisAnimator.SetFloat("MoveSpeed", MoveSpeed / (CurMoveSpeed * 2));
            }
            ThisAnimator.SetBool("IsMove", true);
        }
        else
        {
            ThisAnimator.SetBool("IsMove", false);
        }
    }
    void UpdatePlayerState()
    {
        switch (PlayerStateType)
        {
            case Player.PlayerStateTypeEnum.Idle:
                if (IsCelebrate)
                {
                    ThisAnimator.SetBool("IsCelebrate", true);
                    PlayerStateType = Player.PlayerStateTypeEnum.Celebrate;
                }
                else if (IsBeat)
                {
                    ThisAnimator.SetBool("IsBeat", true);
                    PlayerStateType = Player.PlayerStateTypeEnum.Beat;
                }
                else if (IsReload)
                {
                    ThisAnimator.SetBool("IsReload", true);
                    curReloadGunI = CurGunI;
                    PlayerStateType = Player.PlayerStateTypeEnum.Reload;
                }
                else if (IsAttack || IsViolent)
                {
                    ThisAnimator.SetBool("IsAttack", true);
                    PlayerStateType = Player.PlayerStateTypeEnum.Attack;
                }
                break;
            case Player.PlayerStateTypeEnum.Attack:
                if (IsBeat)
                {
                    ThisAnimator.SetBool("IsAttack", false);
                    ThisAnimator.SetBool("IsBeat", true);
                    PlayerStateType = Player.PlayerStateTypeEnum.Beat;
                }
                else if ((!IsAttack && !IsViolent) || IsReload || IsCelebrate)
                {
                    ThisAnimator.SetBool("IsAttack", false);
                    PlayerStateType = Player.PlayerStateTypeEnum.Idle;
                }
                else if (IsAttack)
                {
                    ThisTF.rotation = Quaternion.Slerp(ThisTF.rotation, Quaternion.LookRotation(AttackTargetTF.position - ThisTF.position), DataManager.DeltaTime * 10);
                }
                break;
            case Player.PlayerStateTypeEnum.Reload:
                if (curReloadGunI != CurGunI || IsBeat || IsCelebrate)
                {
                    ThisAnimator.SetBool("IsReload", false);
                    IsReload = false;
                    PlayerStateType = Player.PlayerStateTypeEnum.Idle;
                }
                else if (CurAnimatorStateInfo.normalizedTime > 1 && CurAnimatorStateInfo.IsName("Reload" + CurGunType))
                {
                    ThisAnimator.SetBool("IsReload", false);
                    IsReload = false;
                    IsReloadSuccess = true;
                    PlayerStateType = Player.PlayerStateTypeEnum.Idle;
                }
                break;
            case Player.PlayerStateTypeEnum.Beat:
              
                break;
            case Player.PlayerStateTypeEnum.Celebrate:
              
                break;
            case Player.PlayerStateTypeEnum.Dead:
               
                break;
        }
    }

    bool IsMaterialRed;
    float MaterialChangeTime;
   
    void UpdateMaterial()
    {
        if (IsMaterialRed)
        {
            MaterialChangeTime -= DataManager.DeltaTime;
            if (MaterialChangeTime <= 0)
            {
                PlayerRenderer.material.color = DataManager.ColorOriginal;
                IsMaterialRed = false;
                GamePanel.Self.HurtBgObj.SetActive(false);
            }
        }
    }


    bool IsGunLight;
    float GunLightTime;
 
    void UpdateGunLignt()
    {
        if (IsGunLight)
        {
            GunLightTime -= DataManager.DeltaTime;
            if (GunLightTime <= 0)
            {
                GunLightObj.SetActive(false);
                IsGunLight = false;
            }
        }
    }

    bool IsBombPropEffectLoop;
    float TempBombPropEffectTime;
    GameObject BombPropEffectObj;
    ParticleSystem BombPropEffectParticle;
    void UpdateBombPropEffect()
    {
        if (IsBombPropEffectLoop)
        {
            TempBombPropEffectTime -= DataManager.DeltaTime;
            if (TempBombPropEffectTime <= 0)
            {
                IsBombPropEffectLoop = false;
                BombPropEffectParticle.loop = false;
                BombPropEffectObj.GetComponent<BombEffect>().IsOver = true;
            }
        }
    }


    float TempViolentTime;

    float ViolentTime = 8;
    void UpdateViolent()
    {
        if (IsViolent)
        {
            TempViolentTime -= DataManager.DeltaTime;
            if (TempViolentTime <= 0)
            {
                IsViolent = false;
                ViolentEffectObj.SetActive(false);
            }
            else
            {
                CurViolent = (int)(MaxViolent * TempViolentTime / ViolentTime);
            }
        }
    }
 
    //private int CurHP;
    //private int HurtNum;

    public void MaterialChange()
    {
        if (MaterialChangeTime <= 0)
        {
            PlayerRenderer.material.color = DataManager.ColorRed;
            IsMaterialRed = true;
            MaterialChangeTime = 0.35f;
            //播放受伤音效//
            DataManager.Self.SetPlayEffectAC(HurtAudioClip);
            GamePanel.Self.HurtBgObj.SetActive(true);
        }
        else
        {
            MaterialChangeTime = 0.35f;
        }
    }
    public void ReceiveAttack(int attack)
    {
        if (DataManager.Self.IsGameWin || IsBuffShield || IsCelebrate || CurHP <= 0)
            return;
        if (DataManager.CurChapterLevel == 0 && DataManager.CurChapterType == ChapterTypeEnum.Main)
        {
            attack = 1;
        }
        CurHP -= attack;
        HurtNum++;
        MaterialChange();
        if (CurHP <= 0)
        {
            ThisAnimator.SetBool("IsDead", true);
            PlayerStateType = Player.PlayerStateTypeEnum.Dead;
        }

    }


    void UpdateCheckAllBuff()
    {
    }

   
    void Update()
    {
        UpdateCommon();
        UpdatePlayerState();
        //    UpdateMaterial();
        //     UpdateGunLignt();
        //     UpdateBombPropEffect();
        //    UpdateCheckAllBuff();
        //   UpdateViolent();
    }


    public override void Attack(Transform tf)
    {
        IsAttack = true;
        AttackTargetTF = tf;
    }
    public override void DisAttack()
    {
        IsAttack = false;
    }
    */
}
