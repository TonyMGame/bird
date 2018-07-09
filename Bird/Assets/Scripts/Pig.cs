using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float maxSpeed = 10;

    public float minSpeed = 5;

    private SpriteRenderer sr;   //属性

    public Sprite sp;   //自定义的一个属性 装在图片精灵

    public GameObject boom;   //制作成prefabs

    public GameObject score;  //制作成prefabs

    public bool isPig = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();   //获取SpriteRendere组件的属性Sprite
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude >= maxSpeed) //死亡 
        {
            Dead();
        }
        else if (collision.relativeVelocity.magnitude >= minSpeed && collision.relativeVelocity.magnitude <= maxSpeed) //受伤
         {
            sr.sprite = sp;
         }
    }

    void Dead()
    {

        print("特效");
        if (isPig)
        {
            GameManager.instance.pigs.Remove(this);
        }
        Destroy(gameObject);  //自己消失
        GameObject bo = Instantiate(boom, transform.position, Quaternion.identity); //出现
        GameObject go = Instantiate(score, transform.position,Quaternion.identity);  
        Destroy(go,2f);

    }


}
