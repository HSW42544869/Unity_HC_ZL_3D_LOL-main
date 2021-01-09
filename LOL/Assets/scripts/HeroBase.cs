using UnityEngine;

public class HeroBase : MonoBehaviour
{
    [Header("英雄資料")]
    public HeroData Data;

    private Animator ani;
    protected float[] skillTimer = new float[4];
    protected bool[] skillStart = new bool[4];

    private Rigidbody rig;

    protected virtual void Awake()
    {
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }
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


                if (skillTimer[i] >= Data.skills[i].cd)
                {
                    skillTimer[i] = 0;
                    skillStart[i] = false;
                }
            }
        }

    }
    public void Move(Transform target)
    {
        Vector3 pos = rig.position;

        rig.MovePosition(target.position);

        transform.LookAt(target);

        ani.SetBool("跑步", rig.position != pos);
    }
    public void skill1()
    {
        if (skillStart[0]) return;
        ani.SetTrigger("大");
        skillStart[0] = true;
    }
    public void skill2()
    {
        if (skillStart[1]) return;
        ani.SetTrigger("一招");
        skillStart[1] = true;
    }
    public void skill3()
    {
        if (skillStart[2]) return;
        ani.SetTrigger("二招");
        skillStart[2] = true;
    }
    public void skill4()
    {
        if (skillStart[3]) return;
        ani.SetTrigger("打");
        skillStart[3] = true;
    }

}
