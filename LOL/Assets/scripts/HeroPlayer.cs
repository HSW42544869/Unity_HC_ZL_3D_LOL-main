using UnityEngine.UI;
using UnityEngine;

// : 父類別 - 繼承
// 繼承 : 擁有父類別所有成員
public class HeroPlayer : HeroBase
{
    public Button btnSkill1;
    public Button btnSkill2;
    public Button btnSkill3;
    public Button btnSkill4;

    private Image[] imgSkills = new Image[4];
    private Text[] textSkills = new Text[4];
    private Image[] imgSkillsCD = new Image[4];
    /// <summary>
    /// 目標物件
    /// </summary>
    private Transform target;
    /// <summary>
    /// 虛擬搖桿
    /// </summary>
    private Joystick joy;
    /// <summary>
    /// 攝像機根物件
    /// </summary>
    private Transform camRoot;

    [Header("移動的距離"), Range(0f, 5f)]
    public float moveDistance = 2;

    //override 複寫 - 可以複寫父類別包含 virtual 的成員
    protected override void Awake()
    {
        base.Awake();

        target = GameObject.Find("目標物").transform;
        joy = GameObject.Find("虛擬搖桿").GetComponent<Joystick>();
        camRoot = GameObject.Find("攝影機根物件").transform;

        SetSkillUI();
    }

    protected override void Update()
    {
        base.Update();
        MoveControl();
        UpdateSkillCD();
    }

    /// <summary>
    /// 移動控制
    /// </summary>
    private void MoveControl()
    {
        float v = joy.Vertical;
        float h = joy.Horizontal;
        // 目標物.座標 = 角色.座標 + 角色.前方 * 垂直 * 距離 + 色.右邊 - 水平 * 距離
        target.position = transform.position + camRoot.forward * v * moveDistance + camRoot.right * h * moveDistance;
        //移動目標物件
        Move(target);

    }
    /// <summary>
    /// 設定四個技能按鈕
    /// </summary>
    private void SetSkillUI()
    {

        //取得四個技能按鈕
        btnSkill1 = GameObject.Find("大").GetComponent<Button>();
        btnSkill2 = GameObject.Find("技能1").GetComponent<Button>();
        btnSkill3 = GameObject.Find("技能2").GetComponent<Button>();
        btnSkill4 = GameObject.Find("技能3").GetComponent<Button>();
        // 按鈕 點擊後執行 (方法)
        btnSkill1.onClick.AddListener(Skill1);
        btnSkill2.onClick.AddListener(Skill2);
        btnSkill3.onClick.AddListener(Skill3);
        btnSkill4.onClick.AddListener(Skill4);



        for (int i = 0; i < 4; i++)
        {
            imgSkills[i] = GameObject.Find("技能圖" + (i + 1)).GetComponent<Image>();
            imgSkillsCD[i] = GameObject.Find("冷卻圖" + (i + 1)).GetComponent<Image>();
            textSkills[i] = GameObject.Find("冷卻時間" + (i + 1)).GetComponent<Text>();

            imgSkills[i].sprite = data.skills[i].image;
            textSkills[i].text = "";
        }
    }
    /// <summary>
    /// 更新介面冷卻效果
    /// </summary>
    private void UpdateSkillCD()
    {
        for (int i = 0; i < 4; i++)
        {
            if (skillStart[i])
            {   //技能文字，文字 = (冷卻時間 _ 計時器).轉為字串("F0") -F 後面的數字代表小數點位數
                textSkills[i].text = (data.skills[i].cd - skillTimer[i]).ToString("F0");
                //冷卻圖 .填滿 = 倒數時間 / 冷卻時間
                imgSkills[i].fillAmount = (data.skills[i].cd - skillTimer[i]) / data.skills[i].cd;
            }
            else textSkills[i].text = "";
            imgSkills[i].fillAmount = 0;
        }
    }
}
