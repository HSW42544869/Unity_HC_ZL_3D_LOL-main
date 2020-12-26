using UnityEngine;

public class HeroBase : MonoBehaviour
{
    [Header("英雄資料")]
    public HeroData Data;

    private Animator ani;
    private float[] skillTime = new float[4];
    private bool[] skillstart = new bool[4];

    protected virtual void Awake()
    {
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        TimerControl();
    }

    public void TimerControl()
    {
        for (int i = 0; i < 4; i++)
        {
            if (skillstart[i])
            {
                skillTime[i] += Time.deltaTime;


                if (skillTime[i] >= Data.skills[i].cd)
                {
                    skillTime[i] = 0;
                    skillstart[i] = false;
                }
            }
        }

    }
    public void Move()
    {

    }
    public void skill1()
    {
        if (skillstart[0]) return;
        ani.SetTrigger("大");
        skillstart[0] = true;
    }
    public void skill2()
    {
        if (skillstart[1]) return;
        ani.SetTrigger("一招");
        skillstart[1] = true;
    }
    public void skill3()
    {
        if (skillstart[2]) return;
        ani.SetTrigger("二招");
        skillstart[2] = true;
    }
    public void skill4()
    {
        if (skillstart[3]) return;
        ani.SetTrigger("打");
        skillstart[3] = true;
    }

}
