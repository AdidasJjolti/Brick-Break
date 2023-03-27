using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBrick : Brick
{
    //public override void SetHPBar(GameObject hpBar)
    //{
    //    _hpBar = hpBar.GetComponent<Slider>();
    //}

    Paddle _paddle;

    void Start()
    {
        _paddle = FindObjectOfType<Paddle>();
    }

    void OnDisable()
    {
        switch(Type)
        {
            case eBrickType.PADDLE_LENGTHEN:
                _paddle.SendMessage("LengthenPaddle");
                break;
        }
        DisableHPBar();
    }
}
