using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class CannonAI : MonoBehaviour
{
    [SerializeField] private Vector3 ballPlacement;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int ballCount;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float mainDamage;
    [SerializeField] private float cooldownTime;

    private Queue<GameObject> ballPooling;
    private List<GameObject> enemies;
    private bool isFiring = false;

    private void Start()
    {
        ballPooling = new Queue<GameObject>();
        enemies = new List<GameObject>();

        for (int i = 0; i < ballCount; i++)
        {
            GameObject instanceBall = Instantiate(ballPrefab, ballPlacement, Quaternion.identity);
            instanceBall.transform.SetParent(transform);
            instanceBall.transform.localPosition = ballPlacement;
            instanceBall.GetComponent<Bullet>().Damage = mainDamage;
            ballPooling.Enqueue(instanceBall);
            instanceBall.SetActive(false);
        }
    }

    private void Update()
    {
        SortTargetsByDistance();
        if (enemies.Count > 0)
        {
            transform.DOLookAt(enemies[0].transform.position, 0.02f);
            
          
            if (!isFiring)
            {
                StartFire();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Add(other.gameObject);
            other.GetComponent<AI>().ondeath += EnemyDeath;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
            other.GetComponent<AI>().ondeath -= EnemyDeath;
        }
    }

    public void EnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
        enemy.GetComponent<AI>().ondeath -= EnemyDeath;
    }

    public void SortTargetsByDistance()
    {
        enemies = enemies.OrderBy(e => (e.transform.position - GameManager.instance.Mainland.transform.position).sqrMagnitude).ToList();
    }

    public GameObject GetFromPool()
    {
        if (ballPooling.Count > 0)
        {
            GameObject bullet = ballPooling.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }

        return null;
    }

    private void StartFire()
    {
        if (enemies.Count > 0 && ballPooling.Count > 0 && !isFiring)
        {
            isFiring = true; 
            StartCoroutine(FireRoutine());
        }
    }

    private IEnumerator FireRoutine()
    {
       
        GameObject bullet = GetFromPool();

        if (bullet != null)
        {
            Vector3 shotDirection = (enemies[0].transform.position - transform.position).normalized;
            gameObject.transform.DOShakePosition(0.05f, 1f, 1);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = shotDirection * bulletSpeed;

            
            StartCoroutine(Pooler(bullet));
        }

        
        yield return new WaitForSeconds(cooldownTime);
       

        isFiring = false; 
    }

    private IEnumerator Pooler(GameObject bullet)
    {
       
        yield return new WaitForSeconds(2f);
        bullet.SetActive(false);
        ballPooling.Enqueue(bullet);
        bullet.transform.localPosition = ballPlacement;
    }
}
