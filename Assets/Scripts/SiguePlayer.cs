using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiguePlayer : MonoBehaviour
{
    public GameObject player;
    Vector3 distancia; //Distacia al player
    void Start()
    {
        //player = GameObject.Find("Player");

        distancia = this.transform.position - player.transform.position;
    }

    void Update()
    {

        this.transform.position = player.transform.position + distancia;
    }
}
