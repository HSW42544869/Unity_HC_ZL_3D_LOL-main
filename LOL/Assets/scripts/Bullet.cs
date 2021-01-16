using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    public float speed;
    public float atk;
    
    private void Track()
    {
        Vector3 posA = target.position;                //  目標座標
        Vector3 posB = transform.position;             //  攝影機座標

        posB = Vector3.Lerp(posB, posA, 0.5f * Time.deltaTime * speed);   //差值
        transform.position = posB;
        if (Vector3.Distance(posB,posA)<1)
        {
            target.GetComponent<HeroBase>().Damage(atk);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Track();
    }
}
