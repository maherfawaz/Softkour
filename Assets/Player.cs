using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float speed = 20f;
    public float jumpVel = 20f;
    public float mouseHMult = 180;
    public float mouseVMult = 90;
    public float camPitchRange = 45;
    public Transform camTrans;

    Rigidbody rigid;
    bool jumpNextFixedUpdate = false;

    public Scene scene;
    public int sceneCurrent;
    public int sceneNext;

    void Start() {
        rigid = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        scene = SceneManager.GetActiveScene();
        sceneCurrent = scene.buildIndex;
        sceneNext = scene.buildIndex + 1;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && rigid.velocity.y > -2 && rigid.velocity.y <= 0) {
            jumpNextFixedUpdate = true;
        }
    }

    void FixedUpdate() {
        // Input
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");
        float mouseH = Input.GetAxis("Mouse X");
        float mouseV = Input.GetAxis("Mouse Y");
        //Debug.Log($"H:{hAxis:0.00}      V:{vAxis:0.##}");

        // Rotation of the Player
        Vector3 rot = transform.eulerAngles;
        rot.y += mouseH * mouseHMult * Time.fixedDeltaTime;
        transform.eulerAngles = rot;

        Vector3 rotCam = camTrans.eulerAngles;
        while (rotCam.x > 180) {
            rotCam.x -= 360;
        }
        rotCam.x += -mouseV * mouseVMult * Time.fixedDeltaTime;
        rotCam.x = Mathf.Clamp(rotCam.x, -camPitchRange, camPitchRange);
        camTrans.eulerAngles = rotCam;

        // velocity of the Player
        Vector3 vel = new Vector3();
        vel += transform.forward * vAxis;
        vel += transform.right * hAxis;
        if (vel.magnitude > 1) vel.Normalize();
        vel *= speed; // vel = vel * speed;
        
        // Pull the Y velocity from the Rigidbody
        vel.y = rigid.velocity.y;
        
        // Jump if we need to
        if (jumpNextFixedUpdate) {
            vel.y = jumpVel;
            jumpNextFixedUpdate = false;
        }
        
        rigid.velocity = vel;
    }

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.CompareTag("DeathFloor") || coll.gameObject.CompareTag("Enemy")) SceneManager.LoadScene(sceneCurrent);
        if (coll.gameObject.CompareTag("Goal")) SceneManager.LoadScene(sceneNext);
    }

    private void OnDrawGizmos() {
        if (rigid != null) {
            Gizmos.color = Color.red;
            Vector3 velPos = transform.position + rigid.velocity;
            Gizmos.DrawLine(transform.position, velPos);
            Gizmos.DrawSphere(velPos, 0.5f);
        }
    }
}