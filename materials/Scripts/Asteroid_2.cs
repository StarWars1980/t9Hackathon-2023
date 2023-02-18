using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_2 : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 6.0f;
    public float minsize = 3.0f;
    public float maxsize = 8.0f;
    public float speed = 50.0f;
    public float maxLifetime = 30.0f;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    //private BoxCollider2D _boxCollider;
    // Start is called before the first frame update
    private void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        //_boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction){
        _rigidbody.AddForce(direction * this.speed);

        Destroy(this.gameObject, this.maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Bullet"){
            if((this.size *0.5f) >= this.minsize){
                CreateSplit();
                CreateSplit();
            }
            FindObjectOfType<GameManager>().Asteroid_2Destroyed(this);
            Destroy(this.gameObject);
        }
    }
    
    private void CreateSplit(){
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid_2 half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized * 5);
    }
}
