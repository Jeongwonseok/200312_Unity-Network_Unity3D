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

    // 데미지 손실 함수
    public void TakeDamage(int amount)
    {
        if(!isServer)
        {
            return;
        }
        // 데미지 계산 코드
        currentHealth -= amount;
    }

    void OnChangeHealth(int health)
    {
        healthSlider.value = health;
    }
}
