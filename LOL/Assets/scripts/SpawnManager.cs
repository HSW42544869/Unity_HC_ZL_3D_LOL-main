using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("生成物件")]
    public GameObject goSpawn;
    [Header("生成點")]
    public Transform pointSpawn;
    [Header("生成間隔"), Range(0, 30)]
    public float interval = 10;
    [Header("每一次小兵生成間隔"), Range(0, 10)]
    public float interva1Once = 0.2f;
    [Header("每一次小兵生數量"), Range(0, 10)]
    public int count = 4;
    /// <summary>
    /// 當前小兵數
    /// </summary>
    private int courrent;
    /// <summary>
    /// 生成小兵
    /// </summary>
    private void Spawn()
    {
        if (courrent < count)
        {
            Instantiate(goSpawn, pointSpawn.position, pointSpawn.rotation);
            courrent++;
            Invoke("Spawn", interva1Once);
        }
        else courrent = 0;
    }
    private void Update()
    {
        
    
    }
   
    private void Start()
    {
        InvokeRepeating("Spawn", 0, interval);
    }


}
