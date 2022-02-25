using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour {
    // Start is called before the first frame update
    private void Awake() {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(transform.GetChild(0));   // Camera
        DontDestroyOnLoad(transform.GetChild(1));   // Background
    }
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    public void StartGame() {
        Debug.Log("START");
        SceneManager.LoadScene(1); // Game
    }
}
