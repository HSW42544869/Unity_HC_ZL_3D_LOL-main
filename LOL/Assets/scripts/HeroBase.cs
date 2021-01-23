using UnityEngine;
using UnityEngine.UI;

public class HeroBase : MonoBehaviour
{
    [Header("角色資料")]
    public HeroData data;
    /// <summary>
    /// 動畫控制器
    /// </summary>
    protected Animator ani;
    /// <summary>
    /// 技能計時器:累加時間用
    /// </summary>
    protected float[] skillTimer = new float[4];
    /// <summary>
    /// 技能是否開始
    /// </summary>
    protected bool[] skillStart = new bool[4];
    /// <summary>
    /// 普攻計時器
    /// </summary>
    protected float timer;

    private Rigidbody rig;
    public float hp;
    /// <summary>
    /// 畫布血條
    /// </summary>
    [Header("圖層")]
    public int layer;
    [Header("重生點")]
    public Transform restart;
    /// <summary>
    /// 騷寶血條
    /// </summary>
    protected Transform canvasHP;
    private Text texthp;
    private Image imgHp;
    private float hpMax;



    //protected 保護 - 允許子類別存取
    //virtual 虛擬 - 允許子類別複製
    protected virtual void Awake()
    {
        hp = data.HP;
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        //取得騷寶並更新血條文字
        canvasHP = transform.Find("畫布血條");
        texthp = canvasHP.Find("血條文字").GetComponent<Text>();
        texthp.text = data.HP.ToString();
        imgHp = canvasHP.Find("血條").GetComponent<Image>();
    }
    public float restatrtTime = 3;
    protected void Dead(bool needRestart = true)
    {
        hp = 0;
        enabled = false;
        ani.SetBool("死亡開關", true);
        gameObject.layer = 0;


        if (needRestart) Invoke("Restart", restatrtTime);
        else Destroy(gameObject, 1.5f);
    }
    public void Start()
    {
        hp = data.HP;
        hpMax = hp;

    }
    private void Restart()
    {
        hp = hpMax;
        texthp.text = hp.ToString();
        imgHp.fillAmount = 1;
        enabled = true;
        transform.position = restart.position;
        gameObject.layer = 0;
        ani.SetBool("死亡開關", false);
    }
    public virtual void Damage(float damage)
    {
        hp -= damage;
        texthp.text = hp.ToString();
        imgHp.fillAmount = hp / hpMax;

        if (hp <= 0) Dead();
    }
    public float restartTime = 3;
    protected virtual void Update()
    {
        TimerControl();
    }

    public void TimerControl()
    {
        for (int i = 0; i < 4; i++)
        {
            if (skillStart[i])
            {
                skillTimer[i] += Time.deltaTime;

                // 如果 時間器 >=冷卻時間 就 歸零並且設定為 尚未開始 
                if (skillTimer[i] >= data.skills[i].cd)
                {
                    skillTimer[i] = 0;
                    skillStart[i] = false;
                }
            }
        }

    }
    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="target"></param>
    public virtual void Move(Transform target)
    {

        Vector3 pos = rig.position;
        rig.MovePosition(target.position);//鋼體.移動座標(座標)
        transform.LookAt(target);//看向(目標物件)
        ani.SetBool("跑步", rig.position != pos);//動畫.設定布林值(跑步參數，現在座標 不等於 前面座標)
        canvasHP.eulerAngles = new Vector3(67.7f, 249.048f, 0);//角度不變
    }
    public void Skill1()
    {
        //如果 技能已經開始 就跳出
        if (skillStart[0]) return;
        ani.SetTrigger("大");
        skillStart[0] = true;
    }
    public void Skill2()
    {
        if (skillStart[1]) return;
        ani.SetTrigger("一招");
        skillStart[1] = true;
    }
    public void Skill3()
    {
        if (skillStart[2]) return;
        ani.SetTrigger("二招");
        skillStart[2] = true;
    }
    public void Skill4()
    {
        if (skillStart[3]) return;
        ani.SetTrigger("打");
        skillStart[3] = true;
    }

}
