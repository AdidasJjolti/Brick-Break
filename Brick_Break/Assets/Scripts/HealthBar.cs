using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject _hpSlider;                        // HP�� ������
    List<Transform> _brickList = new List<Transform>();           // HP�ٸ� ǥ���� ���� ��ǥ�� ���� ����Ʈ
    List<GameObject> _hpSliderList = new List<GameObject>();      // ������ HP�� ������ ����Ʈ
    Camera _main;

    void Start()
    {
        _main = Camera.main;
        GameObject[] brickArray = GameObject.FindGameObjectsWithTag("Brick");       // Brick �±׸� ���� ��� ���� ������Ʈ�� ���� �ӽ� �迭 ����
        for(int i = 0; i <brickArray.Length; i++)
        {
            _brickList.Add(brickArray[i].transform);
            GameObject hpSlider = Instantiate(_hpSlider, brickArray[i].transform.position, Quaternion.identity, transform);
            hpSlider.transform.position = _main.WorldToScreenPoint(brickArray[i].transform.position + new Vector3(0, -0.25f, 0));
            hpSlider.GetComponent<Slider>().value = 1;

            var b = brickArray[i].GetComponent<Brick>();
            if (b == null)
            {
                brickArray[i].AddComponent<Brick>();
            }
            b.SetHPBar(hpSlider);

            hpSlider.SetActive(false);
            _hpSliderList.Add(hpSlider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
