using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private int arrowCount;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float mainDamage;
    [SerializeField] private GameObject handPlacement;
    [SerializeField] private Vector3 arrowPlacement;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject fakeArrow;
    [SerializeField] private GameObject arrowPoolPlace;
    [SerializeField] private float cooldown;

    private Queue<GameObject> ballPooling;
    private List<GameObject> enemies;

    public Vector3 shootdirect;
    private bool isFiring = false;
    private bool hasEnemy;

    private void Start()
    {
        ballPooling = new Queue<GameObject>();
        enemies = new List<GameObject>();

        for (int i = 0; i < arrowCount; i++)
        {
            GameObject instanceBall = Instantiate(arrowPrefab, fakeArrow.transform.position, Quaternion.identity);
            instanceBall.transform.SetParent(GameManager.instance.arrowpool.transform);
            
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
            transform.DOLookAt(enemies[0].transform.position, 0.02f,AxisConstraint.Y);
            hasEnemy = true;
            if (!isFiring)
            {
               anim.SetTrigger("shoot");
            }
        }
        else
        {
            hasEnemy = false;
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
            if (enemies.Count == 0)
            {
                isFiring = false;
            }
        }
    }

    public void EnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
        {
            isFiring = false;
        }

        enemy.GetComponent<AI>().ondeath -= EnemyDeath;
    }

    public void SortTargetsByDistance()
    {
        enemies = enemies
            .OrderBy(e => (e.transform.position - GameManager.instance.Mainland.transform.position).sqrMagnitude)
            .ToList();
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

    IEnumerator Pooler(GameObject bullet)
    {
        yield return new WaitForSeconds(2f);
        bullet.SetActive(false);
        ballPooling.Enqueue(bullet);
        bullet.transform.localPosition = arrowPlacement;
    }
    public void StartFire()
    {
        if (hasEnemy && ballPooling.Count > 0 && !isFiring)
        {
            isFiring = true; 
            GameObject bullet = GetFromPool();
            fakeArrow.SetActive(false);   

          
            shootdirect = (enemies[0].transform.position - bullet.transform.position).normalized;

            
            bullet.transform.rotation = Quaternion.LookRotation(shootdirect);
            
          
            bullet.transform.rotation *= Quaternion.Euler(-90, 0, 0); 

           
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.velocity = shootdirect * arrowSpeed;

            
            StartCoroutine(Pooler(bullet));
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        isFiring = false; // Ateşleme işleminden sonra tekrar ateşlenebilir hale getiriyoruz
        if (enemies.Count > 0) // Eğer hala düşman varsa tekrar ateşle
        {
            fakeArrow.SetActive(true);
            anim.SetTrigger("shoot");
        }
    }
}
