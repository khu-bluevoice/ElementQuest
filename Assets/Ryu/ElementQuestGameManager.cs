using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementQuestGameManager : MonoBehaviour
{
    public Dictionary<string, bool> ClearMap = new();
    public static ElementQuestGameManager instance = null;
    
    [SerializeField]
    public bool[] isSpellActive = {true, false, false, true, false, false, true, false, false, true, false, false };

    private void Awake()
    {
        if (null == instance)
        {
            // 씬 시작될때 인스턴스 초기화, 씬을 넘어갈때도 유지되기위한 처리
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // instance가, GameManager가 존재한다면 GameObject 제거 
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {

    }

    // Public 프로퍼티로 선언해서 외부에서 private 멤버변수에 접근만 가능하게 구현
    //public static ElementQuestGameManager Instance
    //{
    //    get
    //    {
    //        if (null == instance)
    //        {
    //            return null;
    //        }
    //        return instance;
    //    }
    //}
}
