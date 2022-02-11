using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 10;
    public float speed = 1.0f;
    public float acceleration = 0.1f;
    public float maxSpeed = 20.0f;

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        OnUpdateBullet();
    }

    protected virtual void OnUpdateBullet() {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    protected void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out Ship ship)) {
            
        }
        Debug.Log(collision.gameObject.name);
        Destroy(gameObject);
    }


    protected void OnBecameInvisible() {
        Destroy(gameObject, 2);
    }
}
