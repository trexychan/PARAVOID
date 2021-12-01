using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
    X,
    Y,
    Z
}

public class LinearMovement : MonoBehaviour {
    public float speed = 1f;
    public float moveBy = 3f;
    public Direction dir_enum;
    
    private Vector3 direction;
    private Rigidbody rb;
    private Rigidbody player_rb;
    private Vector3 og;
    private bool reverse;

    public bool startOpposite;
    private float step;


    //Player Platform variables
    private Vector3 lastPosition;

    void Awake() {
        og = transform.position;
        lastPosition = og;
        //moveBy /= 2;
        reverse = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        switch (dir_enum)
        {
            case Direction.X:
                if (startOpposite)
                {
                    direction = Vector3.left;
                } 
                else
                {
                    direction = Vector3.right;
                }
                break;
            case Direction.Y:
                if (startOpposite)
                {
                    direction = Vector3.down;
                } 
                else
                {
                    direction = Vector3.up;
                }
                break;
            case Direction.Z:
                if (startOpposite)
                {
                    direction = Vector3.back;
                } 
                else
                {
                    direction = Vector3.forward;
                }
                break;
        }

    }

    void Update() { //Y DIRECTION
        //check to see if past moveBy offset
        step = speed * Time.deltaTime; //calculate the stepage
        switch (dir_enum)
        {
            case Direction.X:
                //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(og.x + moveBy, transform.localPosition.y, transform.localPosition.z), step);
                if ((startOpposite ? transform.position.x <= og.x : transform.position.x >= og.x + moveBy) && (startOpposite ? reverse : !reverse))
                {
                    direction = Vector3.left;
                    reverse = true;
                } else if ((!startOpposite ? transform.position.x <= og.x : transform.position.x >= og.x + moveBy) && (startOpposite ? !reverse : reverse))
                {
                    direction = Vector3.right;
                    reverse = false;
                }
                break;
            case Direction.Y:
                //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, og.y + moveBy, transform.localPosition.z), step);
                if ((startOpposite ? transform.position.y <= og.y : transform.position.y >= og.y + moveBy) && (startOpposite ? reverse : !reverse))
                {
                    direction = Vector3.down;
                    reverse = true;
                } else if ((!startOpposite ? transform.position.y <= og.y : transform.position.y >= og.y + moveBy) && (startOpposite ? !reverse : reverse))
                {
                    direction = Vector3.up;
                    reverse = false;
                }
                break;
            case Direction.Z:
                //transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, og.z + moveBy), step);   
                if ((startOpposite ? transform.position.z <= og.z : transform.position.z >= og.z + moveBy) && (startOpposite ? reverse : !reverse))
                {
                    direction = Vector3.back;
                    reverse = true;
                } else if ((!startOpposite ? transform.position.z <= og.z : transform.position.z >= og.z + moveBy) && (startOpposite ? !reverse : reverse))
                {
                    direction = Vector3.forward;
                    reverse = false;
                }
                break;
            
        }

        // if (direction == 1) {
        //     if ((transform.position.y <= og.y + moveBy) && up) {
        //         transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
        //         if (transform.position.y > og.y + moveBy) {
        //             up = false;
        //         }
        //     } 
        //     if (!up) {
        //         transform.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
        //         if (transform.position.y < og.y - moveBy) {
        //             up = true;
        //         }
        //     }
        // }

        // if (direction == 2) { //X POSITION
        //     if ((transform.position.x <= og.x + moveBy) && up) {
        //         transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
        //         if (transform.position.x > og.x + moveBy) {
        //             up = false;
        //         }
        //     } 
        //     if (!up) {
        //         transform.Translate(Vector3.left * Time.deltaTime * speed, Space.World);
        //         if (transform.position.x < og.x - moveBy) {
        //             up = true;
        //         }
        //     }
        // }

        // if (direction == 3) { //Z POSTION 
        //     if ((transform.position.z <= og.z + moveBy) && up) {
        //         transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        //         if (transform.position.z > og.z + moveBy) {
        //             up = false;
        //         }
        //     } 
        //     if (!up) {
        //         transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        //         if (transform.position.z < og.z - moveBy) {
        //             up = true;
        //         }
        //     }
        // }

    

    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * Time.deltaTime * speed);
    }

    private void LateUpdate()
    {
        if (player_rb)
        {
            player_rb.MovePosition(player_rb.position + direction * Time.deltaTime * speed);
        }
        lastPosition = transform.position;
    }

    //Logic to have player actually stick to the moving platform
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player_rb = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player_rb = null;
        }
    }
    

}