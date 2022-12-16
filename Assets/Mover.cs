using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    [Header("Inscribed")]
    public float speed;

    [Header("Dynamic")]
    public Rigidbody rigid;
    public Vector3 velocity;

    void Awake() {
        rigid = GetComponent<Rigidbody>();
        velocity = rigid.velocity;
        velocity.x = speed;
    }

    void FixedUpdate() {
        rigid.velocity = velocity;
    }

    void OnTriggerEnter(Collider coll) {
        if (coll.gameObject.CompareTag("LeftTurn")) {
            if (velocity.z == 0 && velocity.x == speed) {
                velocity.z = speed;
                velocity.x = 0;
                velocity.y = 0;
                return;
            }

            if (velocity.z == speed && velocity.x == 0) {
                velocity.z = 0;
                velocity.x = -speed;
                velocity.y = 0;
                return;
            }

            if (velocity.z == 0 && velocity.x == -speed) {
                velocity.z = -speed;
                velocity.x = 0;
                velocity.y = 0;
                return;
            }

            if (velocity.z == -speed && velocity.x == 0) {
                velocity.z = 0;
                velocity.x = speed;
                velocity.y = 0;
                return;
            }
        }

        if (coll.gameObject.CompareTag("RightTurn")) {
            if (velocity.z == 0 && velocity.x == speed) {
                velocity.z = -speed;
                velocity.x = 0;
                velocity.y = 0;
                return;
            }

            if (velocity.z == -speed && velocity.x == 0) {
                velocity.z = 0;
                velocity.x = -speed;
                velocity.y = 0;
                return;
            }

            if (velocity.z == 0 && velocity.x == -speed) {
                velocity.z = speed;
                velocity.x = 0;
                velocity.y = 0;
                return;
            }

            if (velocity.z == speed && velocity.x == 0) {
                velocity.z = 0;
                velocity.x = speed;
                velocity.y = 0;
                return;
            }
        }

        if (coll.gameObject.CompareTag("YDir")) {
            if (velocity.y == speed) {
                velocity.y = -speed;
                velocity.x = 0;
                velocity.z = 0;
                return;
            }

            if (velocity.y == -speed || velocity.y == 0) {
                velocity.y = speed;
                velocity.x = 0;
                velocity.z = 0;
                return;
            }
        }

        if (coll.gameObject.CompareTag("XDir")) { 
            if (velocity.x == speed) {
                velocity.x = -speed;
                velocity.y = 0;
                velocity.z = 0;
                return;
            }

            if (velocity.x == -speed) {
                velocity.x = speed;
                velocity.y = 0;
                velocity.z = 0;
                return;
            }
        }

        if (coll.gameObject.CompareTag("ZDir")) { 
            if (velocity.z == speed) {
                velocity.z = -speed;
                velocity.y = 0;
                velocity.x = 0;
                return;
            }

            if (velocity.z == -speed || velocity.z == 0) {
                velocity.z = speed;
                velocity.y = 0;
                velocity.x = 0;
                return;
            }
        }
    }
}