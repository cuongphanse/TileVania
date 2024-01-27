using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoment : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]  private float moveSpeed = 1f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, 0f);
    }
    private void OnTriggerExit2D(Collider2D other) {
        moveSpeed = -moveSpeed;
        EnemiFlip();
    }
    void EnemiFlip(){
        transform.localScale = new Vector2 (-Mathf.Sign(rb.velocity.x), 1f);
    }
}
