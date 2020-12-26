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

    protected override void Awake()
    {
        base.Awake();

        Setskill();

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
