using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {
    public static readonly int width = 10;
    public static readonly int height = 20;
    public static Transform[,] board = new Transform[width, height];

    private void Awake() {
        DontDestroyOnLoad(this);
    }

    public void StartGame() {
        Debug.Log("START");
        SceneManager.LoadScene(1); // Game

    }
}
