using UnityEngine;
using UnityEngine.AI;       //引用AI API

public class Solder : HeroBase
{
    /// <summary>
    /// 代理器
    /// </summary>
    private NavMeshAgent agent;

    [Header("對方主堡名稱")]
    public string targetName;
    [Header("停止距離"), Range(0, 10)]
    public float stopDistance = 3;
    [Header("攻擊範圍"), Range(0, 30)]
    public float rangeAttack = 15;
    [Header("敵方的圖層")]
    public int LayerEnemy;
    [Header("對方主堡")]
    private Transform target;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeAttack);
    }
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        agent.stoppingDistance = stopDistance;

        target = GameObject.Find(targetName).transform;
    }
    protected override void Update()
    {
        base.Update();
        Move(target);
    }

    public override void Move(Transform target)
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, rangeAttack, 1 << LayerEnemy);

        if (hit.Length > 0 ) this.target = hit[0].transform;
        
        agent.SetDestination(target.position);
        canvasHP.eulerAngles = new Vector3(65, -90, 0);
        agent.SetDestination(this.target.position);

        ani.SetBool("跑步開關", agent.remainingDistance > agent.stoppingDistance);
    }
}
