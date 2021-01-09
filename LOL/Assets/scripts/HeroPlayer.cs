using UnityEngine.UI;
using UnityEngine;

public class HeroPlayer : HeroBase
{
    public Button betskill1;
    public Button betskill2;
    public Button betskill3;
    public Button betskill4;

    private Image[] imagskills = new Image[4];
    private Text[] Texskills = new Text[4];

    private Transform target;
    private Joystick joy;
    private Transform canRoot;
    [Header("移動的距離"), Range(0f, 5f)]
    public float moveDistance = 2;

    //override 複寫 - 可以複寫父類別包含 virtyal 的成員
    protected override void Awake()
    {
        base.Awake();
        target = GameObject.Find("目標物").transform;
        joy = GameObject.Find("虛擬搖桿").GetComponent<Joystick>();
        canRoot = GameObject.Find("攝影機根物件").transform;
        Setskill();

    }

    protected override void Update()
    {
        base.Update();
        Movecontrol();
        UpdateSkillCD();
    }

    private void UpdateSkillCD()
    {
        for (int i = 0; i < 4; i++)
        {
            if (skillStart[i])
            {
                Texskills[i].text = (Data.skills[i].cd - skillTimer[i]).ToString("F0");
            }
        }
    }
    /// <summary>
    /// 移動控制
    /// </summary>
    private void Movecontrol()
    {
        float v = joy.Vertical;
        float h = joy.Horizontal;
        // 目標物.座標 = 角色.座標 + 角色.前方 * 垂直 * 距離 + 色.右邊 - 水平 * 距離
        target.position = transform.position + canRoot.forward * v * moveDistance + canRoot.right * h * moveDistance;
        //移動目標物件
        Move(target);

    }

    private void Setskill()
    {


        betskill1 = GameObject.Find("大").GetComponent<Button>();
        betskill2 = GameObject.Find("技能1").GetComponent<Button>();
        betskill3 = GameObject.Find("技能2").GetComponent<Button>();
        betskill4 = GameObject.Find("技能3").GetComponent<Button>();

        betskill1.onClick.AddListener(skill1);
        betskill2.onClick.AddListener(skill2);
        betskill3.onClick.AddListener(skill3);
        betskill4.onClick.AddListener(skill4);



        for (int i = 0; i < 4; i++)
        {
            imagskills[i] = GameObject.Find("技能圖" + (i + 1)).GetComponent<Image>();
            Texskills[i] = GameObject.Find("冷卻時間" + (i + 1)).GetComponent<Text>();

            imagskills[i].sprite = Data.skills[i].image;
            Texskills[i].text = "";
        }
    }
}
