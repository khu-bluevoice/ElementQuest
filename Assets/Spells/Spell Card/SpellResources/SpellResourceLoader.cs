using UnityEngine;

public class SpellResourceLoader : MonoBehaviour
{
    [SerializeField]
    Beetlecircus.SerializableDictionary<Element, SpellResourceItem> loaderItems;

    public SpellResourceItem getResourceOfElement(Element e)
    {
        return loaderItems[e];
    }
}
