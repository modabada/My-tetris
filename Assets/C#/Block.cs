using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block: MonoBehaviour {
    private int speed = 1;
    private double[] prevTime = new double[3];

    private void FixedUpdate() {
        if(Time.time - prevTime[0] > (Input.GetKey(KeyCode.DownArrow) ? 1 / speed * 14 : 1 / speed)) {
            MoveDown();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveLeft();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveRight();
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)) {
            Rotation();
        }
    }

    private bool VaildMove() {
        foreach(Transform child in transform) {
            int x = (int) child.position.x;
            int y = (int) child.position.y;
            if(x < 0 || x >= GameManager.width || y < 0 || y >= GameManager.height) {
                return false;
            }
            if(GameManager.board[x, y] != null) {
                return false;
            }
        }
        return true;
    }

    private void MoveDown() {
        transform.position += new Vector3(0, -1, 0);
        if(!VaildMove()) {
            transform.position += new Vector3(0, 1, 0);
        }
        prevTime[0] = Time.time;
    }
    private void MoveLeft() {
        transform.position += new Vector3(-1, 0, 0);
        if(!VaildMove()) {
            transform.position += new Vector3(1, 0, 0);
        }
        prevTime[1] = Time.time;
    }
    private void MoveRight() {
        transform.position += new Vector3(1, 0, 0);
        if(!VaildMove()) {
            transform.position += new Vector3(-1, 0, 0);
        }
        prevTime[2] = Time.time;
    }
    private void Rotation() {
        transform.transform.rotation = Quaternion.identity;
    }
}
