using System.Collections;
using System.Collections.Generic;
using System.Text;
using Meta.WitAi;
using UnityEngine;

public class SpellSelector : MonoBehaviour
{
    [SerializeField]
    public Spell[] spells;
    private List<GameObject> spellGameObjects = new List<GameObject>();

    public GameObject spellCardPrefab;

    float width = 16;
    float height = 30;

    public bool isOpen = false;
    bool isSelecting = false;

    void Start()
    {
        float startX = spells.Length * width / -2 + width / 2;
        for (int i = 0; i < spells.Length; i++)
        {
            Vector3 position = new Vector3(startX + i * width, 0, 0);
            GameObject a = Instantiate(spellCardPrefab, this.transform);
            a.transform.localScale = new Vector3(0.075f, 0.075f, 0.075f);
            a.transform.position += position;
            a.GetComponent<SpellCardUI>().spell = spells[i];
            spellGameObjects.Add(a);
        }

        if (isOpen) this.transform.position = new Vector3(this.transform.position.x, -20, this.transform.position.z);
        else this.transform.position = new Vector3(this.transform.position.x, -50, this.transform.position.z);
    }

    public void SelectSpell(int number)
    {
        GameObject selectedSpell = spellGameObjects[number];
        if (isOpen && selectedSpell)
        {
            isSelecting = true;
            StartCoroutine(_SelectSpell(spellGameObjects, number));
        }
    }

    IEnumerator _SelectSpell(List<GameObject> spells, int number)
    {
        GameObject spell = spells[number];

        float initialScale = spell.transform.localScale.x;

        float from = spell.transform.position.y;
        float to = spell.transform.position.y + 5f;

        float scaleFrom = spell.transform.localScale.x;
        float scaleTo = spell.transform.localScale.x + 0.02f;

        float animtionTime = 5f;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * animtionTime;
            float y = Mathf.Lerp(from, to, t);
            float s = Mathf.Lerp(scaleFrom, scaleTo, t);
            spell.transform.position = new Vector3(spell.transform.position.x, y, spell.transform.position.z);
            spell.transform.localScale = new Vector3(s, s, s);
            yield return null;
        }
        while (t < 2f)
        {
            t += Time.deltaTime * animtionTime;
            yield return null;
        }
        StartCoroutine(_Close(this.transform));
        spell.transform.localPosition = new Vector3(spell.transform.position.x, 0, 0);
        spell.transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        isSelecting = false;
        yield return 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if (isOpen) Close();
        else Open();
    }

    public void Open()
    {
        if (!isOpen && !isSelecting)
            StartCoroutine(_Open(this.transform));
    }

    public void Close()
    {
        if (isOpen && !isSelecting)
            StartCoroutine(_Close(this.transform));
    }

    float animtionTime = 12f;
    IEnumerator _Open(Transform selector)
    {
        float from = -50f;
        float to = -20f;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * animtionTime;
            float y = Mathf.Lerp(from, to, t);
            selector.position = new Vector3(selector.position.x, y, selector.position.z);
            yield return null;
        }
        isOpen = true;

        yield return 0;
    }

    IEnumerator _Close(Transform selector)
    {
        float from = -20f;
        float to = -50f;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * animtionTime;
            float y = Mathf.Lerp(from, to, t);
            selector.position = new Vector3(selector.position.x, y, selector.position.z);
            yield return null;
        }
        isOpen = false;

        yield return 0;
    }
}
