using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField]
    public string spellName;

    [SerializeField]
    public string description;

    [SerializeField]
    public Element element;

    [SerializeField]
    public Spell parentSpell;

    [SerializeField]
    public Sprite spellIcon;

    [SerializeField]
    public int level;

    [SerializeField]
    public Sprite handGesture;

    [SerializeField]
    public int handIndex;
}
