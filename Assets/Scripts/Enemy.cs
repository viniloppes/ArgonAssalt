using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform spawnAtRuntime;
    ScoreBoard scoreBoard;
    [SerializeField] int scoreToIncrease = 15;
    [SerializeField] int hitPoint = 3;

    private void Start()
    {
        scoreBoard = FindAnyObjectByType<ScoreBoard>();
        AddRigidBody();
    }

    private void AddRigidBody()
    {
        Rigidbody tb = gameObject.AddComponent<Rigidbody>();
        tb.useGravity = false;
    }

    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        if (hitPoint < 1)
        {
            GameObject go = Instantiate(deathVFX, transform.position, Quaternion.identity);
            go.transform.parent = spawnAtRuntime;
            Destroy(gameObject);
        }

    }

    private void ProcessHit()
    {
        scoreBoard.IncreaseScore(scoreToIncrease);
        --hitPoint;
        GameObject go = Instantiate(hitVFX, transform.position, Quaternion.identity);
        go.transform.parent = spawnAtRuntime;
    }
}
