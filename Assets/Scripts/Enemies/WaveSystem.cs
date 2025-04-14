using UnityEngine;

using System.Collections;



public class WaveSystem : MonoBehaviour

{

    [SerializeField] private float countdown;

    [SerializeField] private GameObject SpawnPoint;

    public Wave[] waves;

    public int currentWaveIndex = 0;

    public bool EnemiesAlive = false;

    private bool readyToCountDown = true;



    private void Start()

    {

        readyToCountDown = true;

        for (int i = 0; i < waves.Length; i++)

        {

            waves[i].enemiesLeft = waves[i].enemies.Length;

        }

    }



    private void Update()

    {

        if (currentWaveIndex >= waves.Length)

        {

            return;

        }

        if (readyToCountDown == true)

        {

            countdown -= Time.deltaTime;

        }

        if (countdown <= 0 && EnemiesAlive == false)

        {

            readyToCountDown = false;

            countdown = waves[currentWaveIndex].timeToNextWave;

            StartCoroutine(EnemiesAliveDelay());

            StartCoroutine(SpawnWave());

        }

        CheckForEnemy();

        if (waves[currentWaveIndex].enemiesLeft == 0 && EnemiesAlive == false)

        {

            readyToCountDown = true;

            currentWaveIndex++;

        }

    }



    private IEnumerator SpawnWave()

    {

        if (currentWaveIndex < waves.Length)

        {

            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)

            {

                Enemy enemy = Instantiate(waves[currentWaveIndex].enemies[i], SpawnPoint.transform);

                //enemy.TryGetComponent<DmgHp>(out DmgHp dmgHp).SpawnOrigin = gameObject.tag;
                if (enemy.TryGetComponent<DmgHp>(out DmgHp dmgHp))
                {
                    dmgHp.SpawnOrigin = gameObject.tag;
                }
                if (enemy.TryGetComponent<DmgHpFast>(out DmgHpFast dmgHpFast))
                {
                    dmgHpFast.SpawnOrigin = gameObject.tag;
                } 

                

                enemy.transform.SetParent(SpawnPoint.transform);

                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);

            }

        }

    }



    private IEnumerator EnemiesAliveDelay()

    {

        yield return new WaitForSeconds(1f);

        EnemiesAlive = true;

    }



    public void CheckForEnemy()

    {

        GameObject[] RemainingEnemies;

        RemainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (RemainingEnemies.Length == 0)

        {

            EnemiesAlive = false;

        }

        else if (RemainingEnemies.Length >= 0)

        {

            EnemiesAlive = true;

        }

    }

}



[System.Serializable]

public class Wave

{

    public Enemy[] enemies;

    public float timeToNextEnemy;

    public float timeToNextWave;

    public int enemiesLeft;

}