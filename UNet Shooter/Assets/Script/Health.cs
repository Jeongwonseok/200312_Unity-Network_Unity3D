using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;

    // 자동으로 값을 클라이언트에게도 변경해주는 태그  & 훅 걸기
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public Slider healthSlider;

    // NetworkStartPosition 배열 선언
    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        // LocalPlayer 이면 >> 모든 StartPoint를 대입해준다. 
        if(isLocalPlayer)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    // 데미지 손실 함수
    public void TakeDamage(int amount)
    {
        if(!isServer)
        {
            return;
        }
        // 데미지 계산 코드
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            currentHealth = maxHealth;

            // Rpc : 서버에서 발동하면 >> 모든 클라이언트들에서도 자동으로 발동됨
            RpcRespawn();
        }
    }

    void OnChangeHealth(int health)
    {
        healthSlider.value = health;
    }

    // ClientRpc 태그 : 서버에서 발동하면 >> 모든 클라이언트들에서도 자동으로 발동됨
    [ClientRpc]
    void RpcRespawn()
    {
        if(isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;

            // 리스폰포인트 배열의 값이 존재하고, 길이가 0보다 크면 >> 랜덤으로 리스폰 포인트를 지정해주기
            if(spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            // spawnPoint 위치를 지정
            transform.position = spawnPoint;
        }
    }
}
