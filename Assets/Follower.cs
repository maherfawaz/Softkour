using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
    [SerializeField]
    float speed = 1f;
    public Transform player;

    void Update() {
        GameObject player1 = GameObject.FindGameObjectWithTag("Player");
        player = player1.transform;
    }

    void FixedUpdate() {
        transform.LookAt(player);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
}