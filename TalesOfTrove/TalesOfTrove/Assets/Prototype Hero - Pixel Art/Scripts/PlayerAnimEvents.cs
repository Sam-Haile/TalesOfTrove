using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private Player       m_player;
    private AudioManager_Player m_audioManager;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponentInParent<Player>();
        m_audioManager = AudioManager_Player.instance;
    }

    // Animation Events
    // These functions are called inside the animation files
    void AE_resetDodge()
    {
        m_player.ResetDodging();
    }

    void AE_setPositionToClimbPosition()
    {
        m_player.SetPositionToClimbPosition();
    }

    void AE_runStop()
    {
        m_audioManager.PlaySound("RunStop");
    }

    void AE_footstep()
    {
        m_audioManager.PlaySound("Footstep");
    }

    void AE_Jump()
    {
        m_audioManager.PlaySound("Jump");
    }

    void AE_Landing()
    {
        m_audioManager.PlaySound("Landing");
    }

    void AE_Throw()
    {
        m_audioManager.PlaySound("Jump");
    }

    void AE_Parry()
    {
        m_audioManager.PlaySound("Parry");
        m_player.DisableMovement(0.5f);
    }

    void AE_ParryStance()
    {
        m_audioManager.PlaySound("DrawSword");
    }

    void AE_AttackAirSlam()
    {
        m_audioManager.PlaySound("DrawSword");
    }

    void AE_AttackAirLanding()
    {
        m_audioManager.PlaySound("AirSlamLanding");
        m_player.DisableMovement(0.5f);
    }

    void AE_Hurt()
    {
        m_audioManager.PlaySound("Hurt");
    }

    void AE_Death()
    {
        m_audioManager.PlaySound("Death");
    }

    void AE_SwordAttack()
    {
        m_audioManager.PlaySound("SwordAttack");
    }

    void AE_SheathSword()
    {
        m_audioManager.PlaySound("SheathSword");
    }

    void AE_Dodge()
    {
        m_audioManager.PlaySound("Dodge");
    }

    void AE_WallSlide()
    {
        //m_audioManager.GetComponent<AudioSource>().loop = true;
        if(!m_audioManager.IsPlaying("WallSlide")) 
            m_audioManager.PlaySound("WallSlide");
    }

    void AE_LedgeGrab()
    {
        m_audioManager.PlaySound("LedgeGrab");
    }

    void AE_LedgeClimb()
    {
        m_audioManager.PlaySound("RunStop");
    }
}
