using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItem : MonoBehaviour
{
    [SerializeField] Image _imgItem;
    [SerializeField] TextMeshProUGUI _countText;
    //[SerializeField] uint _itemCount;                 // ������ ���� Ƚ�� üũ�� ���� ��� ����, 0�� �Ǹ� ������ ȿ�� UI ����


    public void SetItemImage(Sprite item)
    {
        _imgItem.sprite = item;
        _imgItem.color = new Color(1, 1, 1);
    }

    public void SetItemCount(uint count)
    {
        _countText.text = count.ToString();
    }

    // ���޹��� ���� ������ ȿ�� ī��Ʈ�� �ؽ�Ʈ�� ��ȯ�Ͽ� UI�� �ݿ�
    public void CheckItemCount(uint count)
    {
        _countText.text = count.ToString();
    }
}
