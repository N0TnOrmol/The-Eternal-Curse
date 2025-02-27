using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    Vector3 velocity;
    public float gravity = -9.81f * 2;
    public float speed;
    public GameObject Player;
    public GameObject Stevfe;


    void Update()
    {
        Stevfe.transform.position = Vector3.MoveTowards(Stevfe.transform.position, Player.transform.position, speed);
        velocity.y += gravity * Time.deltaTime;
    }
}
