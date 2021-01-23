using UnityEngine;
using UnityEngine.AI;       //引用AI API

public class Solder : HeroBase
{
    /// <summary>
    /// 代理器
    /// </summary>
    private NavMeshAgent agent;

    [Header("對方主堡")]
    public Transform target;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
    }
    protected override void Update()
    {
        base.Update();
        Move(target);
    }

    public override void Move(Transform target)
    {
        agent.SetDestination(target.position);
    }
}
