using UnityEngine;
using UnityEngine.AI;       //引用AI API

public class Solder : HeroBase
{
    /// <summary>
    /// 代理器
    /// </summary>
    private NavMeshAgent agent;
    /// <summary>
    /// 敵方主堡
    /// </summary>
    private Transform castle;
    [Header("對方主堡名稱")]
    public string targetName;
    [Header("停止距離"), Range(0, 10)]
    public float stopDistance = 3;
    [Header("攻擊範圍"), Range(0, 30)]
    public float rangeAttack = 15;
    [Header("敵方的圖層")]
    public int LayerEnemy;
    [Header("攻擊射線位移")]
    public Vector3 posAtkOffset;
    [Header("攻擊射線長度"),Range(0,30)]
    public float lengthAttack = 3;
    [Header("對方主堡")]
    private Transform target;
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeAttack);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + posAtkOffset, transform.forward * lengthAttack);
    }
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed;
        agent.stoppingDistance = stopDistance;

       castle = GameObject.Find(targetName).transform;
        target = castle;
    }
    protected override void Update()
    {
        base.Update();
        Move(target);
    }

    public override void Move(Transform target)
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, rangeAttack, 1 << LayerEnemy);

        if (hit.Length > 0) this.target = hit[0].transform;
        else this.target = castle;
        
        agent.SetDestination(target.position);
        canvasHP.eulerAngles = new Vector3(65, -90, 0);
        agent.SetDestination(this.target.position);

        ani.SetBool("跑步開關", agent.remainingDistance > agent.stoppingDistance);
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Attack();
        }
    }
    private void Attack()
    {
        if (timer <= data.attack)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            ani.SetTrigger("普攻");
        RaycastHit hit;  //碰撞物件資訊

        //如果 (物理,射線碰撞(中心點+位移，方向，碰撞的物件，長度，圖層))
        // out 將方法資訊儲存在參數內
        if (Physics.Raycast(transform.position + posAtkOffset,transform.forward,out hit, lengthAttack, 1 << LayerEnemy))
        {
            hit.collider.GetComponent<Tower>().Damage(data.attack);
        }
        }
    }
    public override void Damage(float damage)
    {
        base.Damage(damage);
        if (hp <= 0) Dead(false);
    }
}
