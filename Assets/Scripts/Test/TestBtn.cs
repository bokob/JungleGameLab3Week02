using UnityEngine;
using UnityEngine.UI;

public class TestBtn : MonoBehaviour
{
    Button _btn;
    void Start()
    {
        _btn = GetComponent<Button>();
        //_btn.onClick.AddListener(() => { Debug.Log("버튼 테스트"); });
        _btn.onClick.AddListener(OnTestBtn);
    }

    void OnTestBtn()
    {
        Debug.Log("버튼 테스트");
    }
}
