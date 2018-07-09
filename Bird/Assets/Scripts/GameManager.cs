using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager instance;

    public Vector3 originPos; //初始化位置

    //获取ui物体
    public GameObject lose;

    public GameObject win;

    private void Awake()
    {
        instance = this;
        if (birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
       
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i<birds.Count;i++ )
        {
            if(i== 0)  //第一只
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

     public void NextBird()
    {
        if (pigs.Count>0)
        {
            if (birds.Count>0)
            {
                //下一只
                Init();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else
        {
            //赢
            win.SetActive(true);
        }
    }

    public void ShowStart()
    {

    }

}
