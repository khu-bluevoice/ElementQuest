using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BookUIManager : MonoBehaviour
{
    [SerializeField]
    public SpellManager spellManager;

    public Element selectedPage;

    public GameObject iconPrefab;
    public GameObject pageObject;

    public Beetlecircus.SerializableDictionary<Element, GameObject> pageButtons;

    public Sprite activePage;
    public Sprite inactivePage;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Element key in pageButtons.Keys)
        {
            pageButtons[key].GetComponent<Button>().onClick.AddListener(() => ChangePage(key));
        }
        // init data
        ChangePage(Element.Fire);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PrevPage()
    {
        switch (selectedPage)
        {
            case Element.Fire:
                this.selectedPage = Element.Wind;
                break;
            case Element.Water:
                this.selectedPage = Element.Fire;
                break;
            case Element.Earth:
                this.selectedPage = Element.Water;
                break;
            case Element.Wind:
                this.selectedPage = Element.Earth;
                break;
        }
        ChangePage(this.selectedPage);
    }

    public void NextPage()
    {
        switch(selectedPage)
        {
            case Element.Fire:
                this.selectedPage = Element.Water;
                break;
            case Element.Water:
                this.selectedPage = Element.Earth;
                break;
            case Element.Earth:
                this.selectedPage = Element.Wind;
                break;
            case Element.Wind:
                this.selectedPage = Element.Fire;
                break;
        }
        ChangePage(this.selectedPage);
    }

    public void ChangePage(Element page)
    {
        this.selectedPage = page;

        // reset page sprites
        foreach (GameObject button in pageButtons.Values)
        {
            button.GetComponent<Image>().sprite = inactivePage;
        }
        pageButtons[page].GetComponent<Image>().sprite = activePage;

        // remove all skill icons;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Spell Icon"))
        {
            Destroy(obj);
        }

        IEnumerable<Spell> filteredSpells = from spell in spellManager.spells
                                            where spell.element == page
                                            select spell;

        // load spell info from spell Manager
        foreach (Spell spell in filteredSpells)
        {
            GameObject icon = Instantiate(iconPrefab, pageObject.transform);
            SpellIcon spellIcon = icon.GetComponent<SpellIcon>();
            spellIcon.image.sprite = spell.spellIcon;
            spellIcon.spellName.text = spell.spellName.ToString();

            // move by spell level
            if (spell.level == 1)
            {
                icon.transform.localPosition = new Vector3(0, 125, 0);
            }
            else if (spell.level == 2)
            {
                icon.transform.localPosition = new Vector3(0, 25, 0);
            }
            else if (spell.level == 3)
            {
                icon.transform.localPosition = new Vector3(0, -75, 0);
            }
        }
    }
}
