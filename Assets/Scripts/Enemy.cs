using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scoreToIncrease = 15;
    [SerializeField] int hitPoint = 3;
    GameObject spawnAtRuntime;
    ScoreBoard scoreBoard;

    private void Start()
    {
        scoreBoard = FindAnyObjectByType<ScoreBoard>();
        spawnAtRuntime = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    private void AddRigidBody()
    {
        try
        {
            Rigidbody tb = gameObject.AddComponent<Rigidbody>();
            if (tb != null)
            {
                tb.useGravity = false;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);
        }

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
            go.transform.parent = spawnAtRuntime.transform;
            Destroy(gameObject);
        }

    }

    private void ProcessHit()
    {
        scoreBoard.IncreaseScore(scoreToIncrease);
        --hitPoint;
        GameObject go = Instantiate(hitVFX, transform.position, Quaternion.identity);
        go.transform.parent = spawnAtRuntime.transform;
    }
}
