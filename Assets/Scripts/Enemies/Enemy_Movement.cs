using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public GameObject Player;
    public GameObject Stevfe;
    public float speed;

    void Update()
    {
        Stevfe.transform.position = Vector3.MoveTowards(Stevfe.transform.position, Player.transform.position, speed);
        
    }
}
