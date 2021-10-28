using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMovement : MonoBehaviour {
    public float speed;
    public float moveBy;
    public int direction;

    private Vector3 og;
    private bool up;

    void Awake() {
        og = transform.position;
        //moveBy /= 2;
        up = true;
    }

    void Update() { //Y DIRECTION
        if (direction == 1) {
            if ((transform.position.y <= og.y + moveBy) && up) {
                transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
                if (transform.position.y > og.y + moveBy) {
                    up = false;
                }
            } 
            if (!up) {
                transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
                if (transform.position.y < og.y - moveBy) {
                    up = true;
                }
            }
        }

        if (direction == 2) { //X POSITION
            if ((transform.position.x <= og.x + moveBy) && up) {
                transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
                if (transform.position.x > og.x + moveBy) {
                    up = false;
                }
            } 
            if (!up) {
                transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
                if (transform.position.x < og.x - moveBy) {
                    up = true;
                }
            }
        }

        if (direction == 3) { //Z POSTION 
            if ((transform.position.z <= og.z + moveBy) && up) {
                transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
                if (transform.position.z > og.z + moveBy) {
                    up = false;
                }
            } 
            if (!up) {
                transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
                if (transform.position.z < og.z - moveBy) {
                    up = true;
                }
            }
        }

    

    }
    

}