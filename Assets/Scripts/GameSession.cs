using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField]int playerLife = 3;
    int score = 0;
    [SerializeField] TextMeshProUGUI liveText;
    [SerializeField] TextMeshProUGUI scoreText;
    private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        Debug.Log(numGameSessions);
        if(numGameSessions > 1){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        liveText.text = playerLife.ToString();
        scoreText.text = score.ToString();
    }
    // Update is called once per frame
    public void ProcessPlayerDeath(){
        if(playerLife >1){
            TakeLife();
        }else{
            ResetGameSessions();
        }
    }

    public void AddToScore(int pointsToAdd){
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    void TakeLife(){
        playerLife --;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
        liveText.text = playerLife.ToString();
    }
    void ResetGameSessions(){
        FindObjectOfType<ScenePersits>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
