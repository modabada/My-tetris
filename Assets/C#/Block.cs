using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block: MonoBehaviour {
    private double dropTime = 0;
    private readonly bool[] pushed = new bool[] {false, false, false};
    private bool isPlace = false;

    private void Update() {
        if(isPlace) {
            return;
        }

        if(Time.time - dropTime > (Input.GetKey(KeyCode.DownArrow) ? 1 / GameManager.speed / 12 : 1 / GameManager.speed)) {
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
        if(transform.childCount == 0) {
            Destroy(this);
        }

        pushed[0] = left;
        pushed[1] = right;
        pushed[2] = rotate;
    }

    #region 블럭 이동, 회전
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
            PlaceBlock();
            ClearLine();
            isPlace = true;
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
    #endregion

    #region 블럭 시스템
    private void PlaceBlock() {
        foreach(Transform child in transform) {
            GameManager.board[(int) child.position.x, (int) child.position.y] = child;
        }
    }

    private void ClearLine() {
        HashSet<int> posY = new HashSet<int>();
        foreach(Transform child in transform) {
            posY.Add((int) child.position.y);
        }
        foreach(int y in posY) {
            if(!HasEmpty(y)) {
                DeleteLine(y);
            }
        }
    }
    private bool HasEmpty(int y) {
        for(int x = 0; x < GameManager.width; x++) {
            if(GameManager.board[x, y] == null) {
                return true;
            }
        }
        return false;
    }

    private void DeleteLine(int y) {
        for(int x = 0; x < GameManager.width; x++) {
            Destroy(GameManager.board[x, y]);
            for(int d_y = y + 1; d_y < GameManager.height; d_y++) {
                GameManager.board[x, d_y - 1] = GameManager.board[x, d_y];
                GameManager.board[x, d_y - 1].transform.position += new Vector3(0, -1, 0);
            }
        }
    }
    #endregion
}
