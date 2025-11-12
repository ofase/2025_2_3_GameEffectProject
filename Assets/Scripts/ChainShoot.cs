using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ChainShoot : MonoBehaviour
{

    [SerializeField] float refreshRate = 0.01f;
    [SerializeField] [Range(1, 10)] int maximumEnemiesInChain = 3;
    [SerializeField] float delayBetweenEachChain = 0.3f;
    [SerializeField] Transform playerFirePoint;
    [SerializeField] EnemyDetector playerEnemyDectector;
    [SerializeField] GameObject lineRendererPrefab;

    bool shooting;
    bool shot;
    float counter = 1;
    GameObject currentClosestEnemy;
    List<GameObject> spawnedLineRenderers = new List<GameObject>();
    List<GameObject> enemiesInChain = new List<GameObject>();
    List<GameObject> activeEffect = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
            {
            if(playerEnemyDectector.GetEnemiesInRange().Count > 0 )
            {
                if(!shooting)
                {
                    StartShooting();
                }
            }
            else
            {
                StopShooting();
            }
        }
        if (Input.GetButtonUp("Fire1"))
            {
            StopShooting();
        }

    }

    void StopShooting()
    {
        shooting = false;
        shot = false;
        counter = 1;

        for (int i = 0; i < spawnedLineRenderers.Count; i++)
        {
            Destroy(spawnedLineRenderers[i]);
        }

        spawnedLineRenderers.Clear();
        enemiesInChain.Clear();

        for (int i = 0; i < activeEffect.Count; i++)
        {
            Destroy(activeEffect[i]);
        }

        activeEffect.Clear();
    }

    IEnumerator updateLineRendere(GameObject lineR, Transform startPos, Transform endPos, bool getClosestEnemyToPlayer = false)
    {
        if(shooting && shot && lineR != null)
        {
            lineR.GetComponent<LineRendererController>().SetPosition(startPos, endPos);

            yield return new WaitForSeconds(refreshRate);

            if (currentClosestEnemy != playerEnemyDectector.GetClosestEnemy())
            {
                StopShooting();
                StartShooting();
            }
        }
        else
        {
            StartCoroutine(updateLineRendere(lineR, startPos, endPos));
        }
    }

    IEnumerator ChainReaction(GameObject closestEnemy)
    {
        yield return new WaitForSeconds(delayBetweenEachChain);

        if(counter == maximumEnemiesInChain)
        {
            yield return null;
        }
        else
        {
            if(shooting)
            {
                counter++;
                enemiesInChain.Add(closestEnemy);

                if(!enemiesInChain.Contains(closestEnemy.GetComponent<EnemyDetector>().GetClosestEnemy()))
                {
                    NewLineRenderer(closestEnemy.transform, closestEnemy.GetComponent<EnemyDetector>().GetClosestEnemy().transform);
                    StartCoroutine(ChainReaction(closestEnemy.GetComponent<EnemyDetector>().GetClosestEnemy()));
                }
            }
        }
    }

    void NewLineRendere(Transform startPos, Transform endPos, bool getClosestEnemyToPlayer = false)
    {

    }

    void StartShooting()
    {
        shooting = true;

        if(playerEnemyDectector != null && playerFirePoint != null && lineRendererPrefab != null)
        {
            if(!shot)
            {
                shot = true;

                currentClosestEnemy = playerEnemyDectector.GetClosestEnemy();
                NewLineRenderer;

            }
        }

    }
}
