using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip soundPickup;
    // Start is called before the first frame update
    bool wasCollected = false;
    [SerializeField]int pointsPickup = 10;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !wasCollected){
            wasCollected = true;
            FindObjectOfType<GameSession>().AddToScore(pointsPickup);
            AudioSource.PlayClipAtPoint(soundPickup,Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
