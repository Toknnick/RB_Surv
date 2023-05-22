using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    public void TakePlayer(GameObject player)
    {
        this.player = player;
        offset = transform.position - this.player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
