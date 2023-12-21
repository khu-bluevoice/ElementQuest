using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementQuestGameManager : MonoBehaviour
{
    public Dictionary<string, bool> ClearMap = new Dictionary<string, bool>();
    public static ElementQuestGameManager instance = null;

    private void Awake()
    {
        if (null == instance)
        {
            // �� ���۵ɶ� �ν��Ͻ� �ʱ�ȭ, ���� �Ѿ���� �����Ǳ����� ó��
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // instance��, GameManager�� �����Ѵٸ� GameObject ���� 
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {

    }

    // Public ������Ƽ�� �����ؼ� �ܺο��� private ��������� ���ٸ� �����ϰ� ����
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
