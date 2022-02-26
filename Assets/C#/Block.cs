using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block: MonoBehaviour {
    private double speed = 0.8;
    private double dropTime = 0;
    private readonly bool[] pushed = new bool[] {false, false, false};

    private void Update() {
        if(Time.time - dropTime > (Input.GetKey(KeyCode.DownArrow) ? 1 / speed / 12 : 1 / speed)) {
            MoveDown();
        }
        bool left = Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.RightArrow);
        bool rotate = Input.GetKey(KeyCode.UpArrow);
        if(left && !pushed[0]) {
            MoveLeft();
        }
        else if(right && !pushed[1]) {
            MoveRight();
        }
        else if(rotate && !pushed[2]) {
            Rotation();
        }

        pushed[0] = left;
        pushed[1] = right;
        pushed[2] = rotate;
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
        dropTime = Time.time;
    }
    private void MoveLeft() {
        transform.position += new Vector3(-1, 0, 0);
        if(!VaildMove()) {
            transform.position += new Vector3(1, 0, 0);
        }
    }
    private void MoveRight() {
        transform.position += new Vector3(1, 0, 0);
        if(!VaildMove()) {
            transform.position += new Vector3(-1, 0, 0);
        }
    }
    private void Rotation() {
        transform.Rotate(0, 0, 90);
        if(!VaildMove()) {
            transform.Rotate(0, 0, -90);
        }
    }
}
