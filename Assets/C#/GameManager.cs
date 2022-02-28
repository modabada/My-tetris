using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {
    public static readonly int width = 10;
    public static readonly int height = 30;
    public static double speed = 0.8;
    public static double score = 0;
    public static Transform[,] board = new Transform[width, height];


    private void Awake() {
        DontDestroyOnLoad(this);
    }

    public void StartGame() {
        SceneManager.LoadScene(1); // Game
    }

    public static void EndGame() {
        SceneManager.LoadScene(2);
    }
}
