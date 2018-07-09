using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

	//动画播放完后挂载的脚本
    public void Show()
    {
        GameManager.instance.ShowStart();
    }
}
