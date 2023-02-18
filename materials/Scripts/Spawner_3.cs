using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_3 : MonoBehaviour
{
    public Asteroid_3 asteroidPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnDistance = 15.0f;
    public int spawnQuantity = 1;

    private void Start(){
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn(){
        for(int i = 0; i < this.spawnQuantity; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid_3 asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minsize, asteroid.maxsize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
