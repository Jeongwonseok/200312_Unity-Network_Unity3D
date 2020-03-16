 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{

    public GameObject enemyPrefab;

    public int numberOfEnemies;

    // Server가 시작되는 시점에 발동되는 코드 >> 오버라이딩해서 작성
    public override void OnStartServer()
    {
        for(int i=0; i<numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(
                    Random.Range(-8f, 8f),
                    0,
                    Random.Range(-8f, 8f)
                );

            var spawnRotation = Quaternion.Euler(new Vector3(0.0f,Random.Range(0,180),0.0f));

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);

            NetworkServer.Spawn(enemy);
        }
    }
}
