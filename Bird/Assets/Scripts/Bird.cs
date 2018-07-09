using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    private bool isClick = false;

    public Transform rightPos;

    public Transform leftPos;

    public float maxDis = 3;

    [HideInInspector]
    public SpringJoint2D sp;   //弹性组件  小鸟本身的

    private Rigidbody2D rg;     //刚体组件

    public LineRenderer right;

    public LineRenderer left;

    private Trial trial;

    public GameObject boom;



    private void Awake()    //初始化函数，自动调用
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        trial = GetComponent<Trial>();
    }

    private void OnMouseDown()
    {
        print("鼠标按下");
        isClick = true;
        rg.isKinematic = true; //鼠标按下  刚体脱离物理控制，只有重力
    }

    private void OnMouseUp()
    {
        print("鼠标抬起");
        isClick = false;
        rg.isKinematic = false; //受到物理控制
        Invoke("Fly", 0.1f);     //弹性失活
        //禁用划线组件
        right.enabled = false;
        left.enabled = false;

    }

    private void Update()  //类似监听函数
    {
        if (isClick){      //鼠标一直按下，位置跟随                                    //小鸟在世界坐标 
            print(transform);
            print(Camera.main);
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //将鼠标的屏幕坐标转换为世界坐标给目标对象
            //transform.position += new Vector3(0,0,10);                               //将Z轴加上10  远离摄像机
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            if (Vector3.Distance(transform.position,rightPos.position) > maxDis ) {   //位置限定  
                Vector3 pos = (transform.position - rightPos.position).normalized;    //单位化向量      
                pos *= maxDis;    //最大长度向量
                transform.position = pos + rightPos.position;
            }

            Line();
        }
    }
    //飞出
    void Fly()
    {
        trial.TrailStart();
        sp.enabled = false;  //弹性失活
        Invoke("Next", 5);
    }
    //划线
    void Line()
    {
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0,rightPos.position);
        right.SetPosition(1, transform.position);
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    
    void Next()  //下一只飞出 当前作废
    {
        GameManager.instance.birds.Remove(this);
        Destroy(gameObject);

        GameObject bo = Instantiate(boom, transform.position, Quaternion.identity); //出现
        GameManager.instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        trial.TrailClear();
    }

}
