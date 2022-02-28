using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreControler : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = GameManager.score.ToString();
    }

    public void Regame() {
        SceneManager.LoadScene(1);
        GameManager.score = 0;
    }

    public void Exit() {
        Application.Quit();
    }
}
