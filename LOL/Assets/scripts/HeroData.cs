using UnityEngine;

//ScriptableObject
//腳本畫物件，將腳本變為物件存放在專案內方便管理


[CreateAssetMenu(fileName = "英雄資料", menuName = "Wie/英雄資料")]
public class HeroData: ScriptableObject
{
    //血量
    [Header("HP"), Range(100, 800)]
    public float HP;
    //魔力
    [Header("MP"), Range(50, 400)]
    public float MP;
    //普攻攻擊力
    [Header("普攻攻擊力"), Range(20, 100)]
    public float attack;
    //移動速度
    [Header("移動數度"), Range(0.5f, 100f)]
    public float speed;
    [Header("冷卻時間"), Range(0.5f, 100f)]
    public float cd;
    [Header("技能表")]
    public skill[] skills;
}
//序列畫:
[System.Serializable]
public class skill
{
    public string Name;
    [Header("攻擊"), Range(10, 100)]
    public float attack;
    [Header("消耗"), Range(10, 100)]
    public float cost;
    [Header("圖片")]
    public Sprite image;
    [Header("冷卻"), Range(1, 100)]
    public float cd;
}
