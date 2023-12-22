using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject messagePrefab;

    [SerializeField]
    private GameObject canvas;

    private Queue<GameObject> messageQueue = new Queue<GameObject>();

    void Awake()
    {
        Transform mainCameraTransform = Camera.main.transform;
        transform.SetParent(mainCameraTransform, false);
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.

    bool isDisplaying = false;
    private void Update()
    {
        if(!isDisplaying && messageQueue.Count > 0)
        {
            GameObject messageObject = messageQueue.Dequeue();
            StartCoroutine(_ShowMessage(messageObject));
        }
    }

    public void ShowMessage(string message, MessageType messageType = MessageType.NORMAL)
    {
        GameObject messageObject = Instantiate(messagePrefab, canvas.transform);
        messageObject.transform.localPosition += new Vector3(0, 250f, 0);
        messageObject.SetActive(false);

        TMPro.TextMeshProUGUI textObject = messageObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textObject.text = message;

        if (messageType == MessageType.SYSTEM)
        {
            textObject.color = Color.blue;
        }
        else if (messageType == MessageType.ITEM_DROP)
        {
            textObject.color = Color.yellow;
        }
        else
        {
            textObject.color = Color.white;
        }

        messageQueue.Enqueue(messageObject);
    }

    IEnumerator _ShowMessage(GameObject messageObject)
    {
        isDisplaying = true;
        messageObject.SetActive(true);

        float from = 0;
        float to = 1;

        float animtionTime = 10f;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * animtionTime;
            float opacity = Mathf.Lerp(from, to, t);
            Color c = messageObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color;
            c.a = opacity;
            messageObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = c;

            Color b = messageObject.GetComponent<Image>().color;
            b.a = Mathf.Lerp(0, 0.8f, t);
            messageObject.GetComponent<Image>().color = b;
            yield return null;
        }
        while (t < 38f)
        {
            t += Time.deltaTime * animtionTime;
            yield return null;
        }
        while (t < 39f)
        {
            t += Time.deltaTime * animtionTime;
            float opacity = Mathf.Lerp(to, from, t - 45f);
            Color c = messageObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color;
            c.a = opacity;
            messageObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = c;

            Color b = messageObject.GetComponent<Image>().color;
            b.a = Mathf.Lerp(0.8f, 0, t - 45f);
            messageObject.GetComponent<Image>().color = b;

            yield return null;
        }
        Destroy(messageObject);
        while (t < 43f)
        {
            t += Time.deltaTime * animtionTime;
            yield return null;
        }

        isDisplaying = false;
        yield return 0;
    }
    public void SetPos()
    {
        Transform mainCameraTransform = Camera.main.transform;
        transform.SetParent(mainCameraTransform, false);
    }
}