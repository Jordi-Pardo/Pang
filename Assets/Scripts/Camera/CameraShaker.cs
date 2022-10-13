using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private float shakerTimer;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        PlayerShoot.OnPlayerShootRifle += PlayerShoot_OnPlayerShootRifle;
    }

    private void PlayerShoot_OnPlayerShootRifle(int num)
    {
        Shake();
    }

    // Update is called once per frame
    void Update()
    {
        if (shakerTimer > 0)
        {
            shakerTimer -= Time.deltaTime;
            if (shakerTimer <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
    }

    public void CameraShake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakerTimer = time;
    }
    private void Shake()
    {
        CameraShake(.75f, 0.2f);
    }

    private void OnDestroy()
    {
        PlayerShoot.OnPlayerShootRifle -= PlayerShoot_OnPlayerShootRifle;
    }
}
