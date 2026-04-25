using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanderSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;
    [SerializeField] private ParticleSystem middleThrusterParticleSystem;
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;

    PlayerController playerController;

    private void Awake()
    {
       playerController =GetComponent<PlayerController>();

        playerController.OnUpForce += LanderSystem_OnUpForce;
        playerController.OnRightForce += LanderSystem_OnLeftForce;
        playerController.OnLeftForce += LanderSystem_OnRightForce;
        playerController.OnBeforeForce += LanderSystem_OnBeforeForce;
    }
    private void LanderSystem_OnBeforeForce(object sender, EventArgs e)
    {
        SetEnableParticleSystemEnable(leftThrusterParticleSystem, false);
        SetEnableParticleSystemEnable(middleThrusterParticleSystem, false);
        SetEnableParticleSystemEnable(rightThrusterParticleSystem, false);

    }

    private void LanderSystem_OnUpForce(object sender,EventArgs e)
    {
        SetEnableParticleSystemEnable(leftThrusterParticleSystem, true);
        SetEnableParticleSystemEnable(middleThrusterParticleSystem, true);
        SetEnableParticleSystemEnable(rightThrusterParticleSystem, true);

    }
    private void LanderSystem_OnRightForce(object sender, EventArgs e)
    {
        SetEnableParticleSystemEnable(leftThrusterParticleSystem, false);
        SetEnableParticleSystemEnable(middleThrusterParticleSystem, false);
        SetEnableParticleSystemEnable(rightThrusterParticleSystem, true);

    }
    private void LanderSystem_OnLeftForce(object sender, EventArgs e)
    {
        SetEnableParticleSystemEnable(leftThrusterParticleSystem, true);
        SetEnableParticleSystemEnable(middleThrusterParticleSystem, false);
        SetEnableParticleSystemEnable(rightThrusterParticleSystem, false);

    }

    private void  SetEnableParticleSystemEnable(ParticleSystem particleSystem , bool enable)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enable;


    }
    
}
