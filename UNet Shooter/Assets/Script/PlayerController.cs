using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public GameObject bulletPrefab;

    public Transform bulletSpawn;

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    // 코드 발동하는 척 하면서 방장한테 행동을 맡긴다. >> 즉 방장에서 행동이 이루어진다.
    [Command]
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // 호스트에서 발생한 행동을 클라이언트에게 보내준다.
        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}
