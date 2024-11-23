using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour,IDamageable
{

    [SerializeField]private float Health;
    [SerializeField] private GameObject target;
    [SerializeField] private EnemyScriptable Scriptable;
    private Animator anim;
    
    private NavMeshAgent agent;
    
    public Action<GameObject> ondeath;

    private void Awake()
    {
        Health = Scriptable.Health;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(GameManager.instance.Mainland.transform.position);
        agent.speed = Scriptable.Speed;
        
    }

    private void Update()
    {
        if (Health<=0)
        {
            ondeath?.Invoke(gameObject);
            agent.speed = 0;
            anim.SetBool("death",true);

           




        }



        if (agent.remainingDistance <= agent.stoppingDistance)
        {
           
            givedamage();
        }
        
            
            
        
    }

    public void takedamage(float x)
    {
        Health -= x;
        if (Health <= 0)
        {
            GameManager.instance.takeCoin(Scriptable.CoinOnDeath);
        }
    }

    public void givedamage()
    {
        if (Health <= 0)
        {
            
        }
        else
        {
            GameManager.instance.takeDamage(-1);
            Destroy(gameObject);
        }
           
            
       
    }

    public void destroy()
    {
       
        Destroy(gameObject);
        
    }
}
