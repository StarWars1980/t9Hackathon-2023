using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public ParticleSystem explosion;
    public TMP_Text scoreText;
    public TMP_Text livesText;
    public GameObject GameOVERText;
    public float respawnTime = 3.0f;
    public float invincibility = 3.0f;
    public int lives = 3;
    public int score = 0;

     private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return)) {
            NewGame();
        }
    }

    public void NewGame()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        for (int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }

        setScore(0);
        setLives(3);
        Respawn();
    }

    public void AsteroidDestroyed(Asteroid asteroid){
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        if(score >= 500){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        else{
            if(asteroid.size  < 2.0f){
            setScore(score += 100);
            }else if(asteroid.size < 3.0f){
                setScore(score += 50);
            }else{
                setScore(score += 25);
            }
        }
    }

    public void Asteroid_2Destroyed(Asteroid_2 asteroid){
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        if(score >= 500){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        else{
            if(asteroid.size  < 1.0f){
            setScore(score += 100);
            }else if(asteroid.size < 2.0f){
                setScore(score += 50);
            }else{
                setScore(score += 25);
            }
        }
    }

    public void Asteroid_3Destroyed(Asteroid_3 asteroid){
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        if(score >= 500){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        else{
            if(asteroid.size  < 1.0f){
            setScore(score += 100);
            }else if(asteroid.size < 1.2f){
                setScore(score += 50);
            }else{
                setScore(score += 25);
            }
        }
    }

    public void Asteroid_4Destroyed(Asteroid_4 asteroid){
        explosion.transform.position = asteroid.transform.position;
        explosion.Play();
        // if(score >= 750){
        //     //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        // }
        // else{
            if(asteroid.size  < 0.4f){
            setScore(score += 100);
            }else if(asteroid.size < 0.6f){
                setScore(score += 50);
            }else{
                setScore(score += 25);
            }
        //}
    }

    public void PlayerDied(){
        explosion.transform.position = this.player.transform.position;
        explosion.Play();
        setLives(lives-1);

        if(this.lives <= 0){
            GameOver();
        }else{
            Invoke(nameof(Respawn), this.respawnTime);
        }
    }

    private void Respawn(){
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollisions");
        this.player.gameObject.SetActive(true);
        Invoke(nameof(TurnOnCollisions), this.invincibility);
    }

    private void TurnOnCollisions(){
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver(){
        this.lives = 3;
        this.score = 0;
       if(GameOVERText!=null) GameOVERText.SetActive(true);
       //goBackMenu();
        
        Invoke(nameof(TurnOnCollisions), this.invincibility);
    }

    private void setScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void setLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString();
    }
    public void goBackMenu(){
        SceneManager.LoadScene(0);
    }
}
