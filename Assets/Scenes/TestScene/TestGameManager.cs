using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    [SerializeField]
    SpellSelector spellSelector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1키 누름");
            spellSelector.SelectSpell(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2키 누름");
            spellSelector.SelectSpell(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("3키 누름");
            spellSelector.SelectSpell(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("4키 누름");
            spellSelector.SelectSpell(3);
        }
    }
}
