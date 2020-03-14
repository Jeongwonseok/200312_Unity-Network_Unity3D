using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 상대방에게 데미지를 주는 코드
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();

        if(health != null)
        {
            health.TakeDamage(10);
        }

        Destroy(gameObject);
    }
}
