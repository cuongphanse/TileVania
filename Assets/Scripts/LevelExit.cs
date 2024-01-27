using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float sceneDelay = 1f;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){

        StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel(){
        yield return new WaitForSecondsRealtime(sceneDelay);
        int currentSence = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSence + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        FindObjectOfType<ScenePersits>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }
    
}
