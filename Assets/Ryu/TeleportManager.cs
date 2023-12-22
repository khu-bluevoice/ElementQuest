using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject GameCharacter;
    public GameObject CenterEye;
    private UIManager UIManager;

    public float teleportDelay = 1f;
    private float delayCounter = 0;

    private RaycastHit hit;
    private GameObject HitColliderSave;

    private int boxlayerMask;
    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 8;
        boxlayerMask = 1 << 9;
        UIManager = GameObject.FindFirstObjectByType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        delayCounter += Time.deltaTime;

        if (Physics.Raycast(CenterEye.transform.position, CenterEye.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            HitColliderSave = hit.collider.gameObject;
            //Debug.Log("Hit TeleportPoint " + hit.collider.gameObject.name);
            Debug.DrawRay(CenterEye.transform.position, CenterEye.transform.forward * hit.distance, Color.red);

            hit.collider.gameObject.SendMessage("_OnTriggerEnter");

            //Debug.Log(hit.collider.gameObject.name);
        }
        else
        {
            if (HitColliderSave != null)
            {
                HitColliderSave.SendMessage("_OnTriggerExit");
            }
            Debug.DrawRay(CenterEye.transform.position, CenterEye.transform.forward * 1000f, Color.red);
        }
    }

    void Teleport()
    {
        if (delayCounter < teleportDelay) return;
        else delayCounter = 0;

        if (Physics.Raycast(CenterEye.transform.position, CenterEye.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            // Teleport
            //GameCharacter.transform.position = hit.collider.gameObject.transform.position;
            StartCoroutine(_SmoothTeleport(GameCharacter.transform.position, hit.collider.gameObject.transform.position));
        }
        else
        {
            if(Physics.Raycast(CenterEye.transform.position, CenterEye.transform.forward, out hit, 25f, boxlayerMask))
            {
                int cardNum = hit.transform.GetComponent<BoxScript>().CardNum;
                if (ElementQuestGameManager.instance.isSpellActive[cardNum] == false)
                {
                    UIManager.ShowMessage("상자를 발견했다! 새로운 마법을 익혔다!");
                    ElementQuestGameManager.instance.isSpellActive[cardNum] = true;
                    hit.transform.gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(hit.transform.GetChild(0).gameObject);
                }
                else
                {
                    //already get
                    UIManager.ShowMessage("빈 상자다.");
                    hit.transform.gameObject.GetComponent<MeshCollider>().enabled = false;
                    Destroy(hit.transform.GetChild(0).gameObject);
                }
            }
        }
    }

    IEnumerator _SmoothTeleport(Vector3 original, Vector3 destination)
    {
        float animtionTime = 10f;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * animtionTime;
            float x = Mathf.Lerp(original.x, destination.x, t);
            float y = Mathf.Lerp(original.y, destination.y, t);
            float z = Mathf.Lerp(original.z, destination.z, t);

            GameCharacter.transform.position = new Vector3(x, y, z);
            yield return null;
        }
        
        yield return 0;
    }


}
