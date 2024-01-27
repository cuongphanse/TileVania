using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersits : MonoBehaviour
{
    private void Awake() {
        int numScenePersist = FindObjectsOfType<ScenePersits>().Length;
        Debug.Log(numScenePersist);
        if(numScenePersist > 1){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetScenePersist(){
        Destroy(gameObject);
    }
}
