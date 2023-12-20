using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    // 활성화된 스펠 관리
    [SerializeField]
    public Spell[] spells;

    [SerializeField]
    public bool[] isSpellActive;

    [SerializeField]
    private Spell startSpell;

    [SerializeField]
    private Spell endSpell;

    [SerializeField]
    private Spell moveSpell;

    [SerializeField]
    private SpellSelector spellSelector;

    private Spell selectedSpell;
    private List<Spell> selectableSpells = new List<Spell>();

    // Start is called before the first frame update
    void Start()
    {
        HandleGesture();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 동작을 인식
    public void HandleGesture()
    {
        if (!selectedSpell) return;

        selectableSpells = new List<Spell>();

        // 동작이 인식되면, selectedSpell 바뀌고,
        // selectedSpell에 맞춰서 selectableSpells가 업데이트 됨
        if (selectedSpell.level < 3)
        {
            // 연계 가능한 스펠 탐색
            foreach (Spell spell in spells)
            {
                if(spell.parentSpell == selectedSpell)
                    selectableSpells.Add(spell);
            }
        }
        else
        {
            selectableSpells.Add(endSpell);
        }
    }
}
