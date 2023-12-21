using System.Collections;
using System.Collections.Generic;
using System.Text;
using Meta.WitAi;
using UnityEngine;

public class SpellSelector : MonoBehaviour
{
    [SerializeField]
    public List<Spell> spells = new List<Spell>();
    private List<GameObject> spellGameObjects = new List<GameObject>();

    public GameObject spellCardPrefab;

    float width = 200;
    float height = 300;

    float closedY = -500f;
    float openedY = -230f;

    public bool isOpen = false;
    bool isSelecting = false;

    void Start()
    {
        DrawDeck();
    }

    public void DrawDeck()
    {
        // reset existing
        foreach (GameObject obj in spellGameObjects)
            Destroy(obj);
        spellGameObjects = new List<GameObject>();
        // draw deck
        float startX = spells.Count * width / -2 + width / 2;
        for (int i = 0; i < spells.Count; i++)
        {
            Vector3 position = new Vector3(startX + i * width, 0, 0);
            GameObject a = Instantiate(spellCardPrefab, this.transform);
            a.transform.localScale = new Vector3(10, 10, 10);
            a.transform.position += position;
            a.GetComponent<SpellCardUI>().spell = spells[i];
            spellGameObjects.Add(a);
        }

        if (isOpen)
            foreach (GameObject obj in spellGameObjects)
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, openedY, obj.transform.localPosition.z);
        else
            foreach (GameObject obj in spellGameObjects)
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, closedY, obj.transform.localPosition.z);
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

        float from = spell.transform.localPosition.y;
        float to = spell.transform.localPosition.y + 100f;

        float scaleFrom = spell.transform.localScale.x;
        float scaleTo = spell.transform.localScale.x + 0.02f;

        float animtionTime = 10f;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * animtionTime;
            float y = Mathf.Lerp(from, to, t);
            float s = Mathf.Lerp(scaleFrom, scaleTo, t);
            spell.transform.localPosition = new Vector3(spell.transform.localPosition.x, y, spell.transform.localPosition.z);
            spell.transform.localScale = new Vector3(s, s, s);
            yield return null;
        }
        while (t < 3f)
        {
            t += Time.deltaTime * animtionTime;
            yield return null;
        }
        StartCoroutine(CloseAndOpenForSpellSelection());
        spell.transform.localPosition = new Vector3(spell.transform.localPosition.x, 0, spell.transform.localPosition.z);
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

    IEnumerator CloseAndOpenForSpellSelection()
    {
        float from = openedY;
        float to = closedY;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * animtionTime;
            float y = Mathf.Lerp(from, to, t);

            foreach (GameObject obj in spellGameObjects)
            {
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, y, obj.transform.localPosition.z);
            }

            yield return null;
        }
        isOpen = false;
        DrawDeck();
        StartCoroutine(_Open());

        yield return 0;
    }

    public void Toggle()
    {
        if (isOpen) Close();
        else Open();
    }

    public void Open()
    {
        if (!isOpen && !isSelecting)
            StartCoroutine(_Open());
    }

    public void Close()
    {
        if (isOpen && !isSelecting)
            StartCoroutine(_Close());
    }

    float animtionTime = 12f;
    IEnumerator _Open()
    {
        float from = closedY;
        float to = openedY;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * animtionTime;
            float y = Mathf.Lerp(from, to, t);

            foreach (GameObject obj in spellGameObjects)
            {
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, y, obj.transform.localPosition.z);
            }

            yield return null;
        }
        isOpen = true;

        yield return 0;
    }

    IEnumerator _Close()
    {
        float from = openedY;
        float to = closedY;

        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * animtionTime;
            float y = Mathf.Lerp(from, to, t);

            foreach (GameObject obj in spellGameObjects)
            {
                obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, y, obj.transform.localPosition.z);
            }

            yield return null;
        }
        isOpen = false;

        yield return 0;
    }
}
