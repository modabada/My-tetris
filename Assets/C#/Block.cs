using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block: MonoBehaviour {
    private double dropTime = 0;
    private readonly bool[] pushed = new bool[] { false, false, false };
    private bool isPlace = false;
    private void Update() {
        if(isPlace) {
            if(transform.childCount == 0) {
                Destroy(gameObject);
            }
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

        pushed[0] = left;
        pushed[1] = right;
        pushed[2] = rotate;
    }

    #region 블럭 이동, 회전
    private bool VaildMove() {
        foreach(Transform child in transform) {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);
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
            GameManager.board[Mathf.RoundToInt(child.position.x), Mathf.RoundToInt(child.position.y)] = child;
        }
        string s = "";
        for(int y = GameManager.height - 1; y >= 0; y--) {
            for(int x = 0; x < GameManager.width; x++) {
                if(GameManager.board[x, y] != null) {
                    s += "O ";
                }
                else {
                    s += "X ";
                }
            }
            s += "\n";
        }
        Debug.Log(s);
    }

    private void ClearLine() {
        foreach(Transform child in transform) {
            int y = Mathf.RoundToInt(child.position.y);
            if(!HasEmpty(y)) {
                DeleteLine(y);
                RowDown(y);
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
            Destroy(GameManager.board[x, y].gameObject);
            GameManager.board[x, y] = null;
        }
    }
    private void RowDown(int y) {
        for(int d_y = y; d_y < GameManager.height; d_y++) {
            for(int x = 0; x < GameManager.width; x++) {
                if(GameManager.board[x, d_y] != null) {
                    GameManager.board[x, d_y - 1] = GameManager.board[x, d_y];
                    GameManager.board[x, d_y] = null;
                    GameManager.board[x, d_y - 1].transform.position += new Vector3(0, -1, 0);
                }
            }
        }
    }
    #endregion
}
