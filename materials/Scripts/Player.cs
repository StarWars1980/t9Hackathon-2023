using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;

    private Rigidbody2D _rigidbody;
    private bool thrusting;
    private float turnDirection;

    private void Awake(){
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            turnDirection = 1.0f;
        }else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            turnDirection = -1.0f;
        }else{
            turnDirection = 0.0f;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }

    private void FixedUpdate(){
        if(thrusting){
            _rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
        if(turnDirection != 0.0f){
            _rigidbody.AddTorque(turnDirection * this.turnSpeed);
        }
    }

    private void Shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Asteroid"){
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }

}
