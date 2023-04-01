using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItem : MonoBehaviour
{
    [SerializeField] Image _imgItem;
    [SerializeField] TextMeshProUGUI _countText;
    //[SerializeField] uint _itemCount;                 // 아이템 지속 횟수 체크를 위한 멤버 변수, 0이 되면 아이템 효과 UI 삭제


    public void SetItemImage(Sprite item)
    {
        _imgItem.sprite = item;
        _imgItem.color = new Color(1, 1, 1);
    }

    public void SetItemCount(uint count)
    {
        _countText.text = count.ToString();
    }

    // 전달받은 남은 아이템 효과 카운트를 텍스트로 변환하여 UI에 반영
    public void CheckItemCount(uint count)
    {
        _countText.text = count.ToString();
    }
}
