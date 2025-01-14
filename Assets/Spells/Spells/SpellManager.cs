using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField]
    private GameObject TeleportManager;

    [SerializeField]
    private SkillScript SkillManager;

    [SerializeField]
    private Transform MainCamera;

    // 활성화된 스펠 관리
    [SerializeField]
    public Spell[] spells;

    [SerializeField]
    public List<SpellName> spellNames;

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

    private float delay = 0;

    void Start()
    {
        selectedSpell = null;
        UpdateSelectableSpells();
        spellSelector.DrawDeck();
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;

        // 회전 회오리
        this.transform.eulerAngles = new Vector3(0, MainCamera.eulerAngles.y, 0);
        this.transform.localPosition = MainCamera.localPosition - new Vector3(0, 0.3f, 0);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1키 누름");
            HandleSpellDetected(selectableSpells[0].spellName);
            spellSelector.SelectSpell(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2키 누름");
            HandleSpellDetected(selectableSpells[1].spellName);
            spellSelector.SelectSpell(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3키 누름");
            HandleSpellDetected(selectableSpells[2].spellName);
            spellSelector.SelectSpell(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4키 누름");
            HandleSpellDetected(selectableSpells[3].spellName);
            spellSelector.SelectSpell(3);
        }
    }

    void selectSpellOfSelector(SpellName detectedSpell)
    {
        int i = 0;
        foreach (Spell spell in spellSelector.spells)
        {
            if (spell.spellName == detectedSpell)
                spellSelector.SelectSpell(i);
            i++;
        }
    }

    // 동작을 인식
    public void HandleSpellDetected(SpellName detectedSpell)
    {
        if (detectedSpell == SpellName.TELEPORT)
        {
            // n초에 한번씩 인식 가능
            if (delay < 0.2f) return;
            else delay = 0;

            TeleportManager.SendMessage("Teleport");
        }
        else if (detectedSpell == SpellName.SPELL_END)
        {
            // n초에 한번씩 인식 가능
            if (delay < 0.2f) return;
            else delay = 0;

            // 스킬 사용
            SkillManager.CastSkill(selectedSpell.spellName);
            // update spell selector
            selectSpellOfSelector(detectedSpell);
            // reset spells
            selectedSpell = null;
            UpdateSelectableSpells();
        }
        else if (detectedSpell == SpellName.SPELL_START)
        {
            // n초에 한번씩 인식 가능
            if (delay < 0.2f) return;
            else delay = 0;

            selectedSpell = startSpell;
            selectSpellOfSelector(detectedSpell);
            UpdateSelectableSpells();
        }
        else
        {
            // 스킬 사용 업데이트
            int index = spellNames.IndexOf(detectedSpell);
            if (index != -1)
            {
                if (ElementQuestGameManager.instance.isSpellActive[index] && selectableSpells.IndexOf(spells[index]) != -1)
                {
                    // n초에 한번씩 인식 가능
                    if (delay < 0.2f) return;
                    else delay = 0;

                    // 스킬 선택
                    selectedSpell = spells[index];
                    selectSpellOfSelector(detectedSpell);
                    UpdateSelectableSpells();
                }
            }
        }
    }

    private void UpdateSelectableSpells()
    {
        selectableSpells = new List<Spell>();

        if (!selectedSpell)
        {
            // Spell Start && Teleport
            selectableSpells.Add(startSpell);
            //selectableSpells.Add(moveSpell);
        }
        // 동작이 인식되면, selectedSpell 바뀌고,
        // selectedSpell에 맞춰서 selectableSpells가 업데이트 됨
        else if (selectedSpell.level < 3)
        {
            // 연계 가능한 스펠 탐색
            int i = 0;
            foreach (Spell spell in spells)
            {
                if (ElementQuestGameManager.instance.isSpellActive[i] && spell.level == selectedSpell.level + 1 && spell.parentSpell && spell.parentSpell.spellName == selectedSpell.spellName)
                    selectableSpells.Add(spell);
                i++;
            }

            if (selectableSpells.Count == 0)
            {
                // 연계 가능한 스킬이 없으면 스킬 종료
                selectableSpells.Add(endSpell);
                //selectableSpells.Add(moveSpell);
            } else
            {
                if (selectedSpell.level + 1 > 1) selectableSpells.Add(endSpell);
                //selectableSpells.Add(moveSpell);
            }
        }
        else // level 3 selected
        {
            selectableSpells.Add(endSpell);
            //selectableSpells.Add(moveSpell); 
        }
        spellSelector.spells = selectableSpells;
    }
}
