using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npcmove : MonoBehaviour
{
    Player2 hurtTarget;
    // Start is called before the first frame update
    NavMeshAgent agent;
    float startAttackTime = -1000;
    public GameObject prefab;
    void Start()
    {


    }
    private void Awake()
    {
        if (prefab != null)
            Instantiate(prefab, transform);

        agent = GetComponent<NavMeshAgent>();
        GetComponentInChildren<Hit>().OnHit = v => {
            var player = v.GetComponent<Player2>();
            if (player != null)
            {
                if (Time.time > startAttackTime + 4)
                {
                    startAttackTime = Time.time;
                    agent.isStopped = true;
                    GetComponentInChildren<Animator>().SetTrigger("attack");
                    hurtTarget = player;
                }
            }
        };
    }

    public void doHurt()
    {
        hurtTarget.hurt();
        hurtTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < startAttackTime + 4)
        {
            //    GetComponentInChildren<Animator>().SetFloat("walkSpeed",0);
            //    GetComponentInChildren<Animator>().SetFloat("motionTime", 1);
            return;
        }

        //GetComponentInChildren<Animator>().SetFloat("walkSpeed", 1);
        //GetComponentInChildren<Animator>().SetFloat("motionTime", 4);
        agent.isStopped = false;
        agent.destination = GameMgr.Instance.players[0].transform.position;

    }
}
