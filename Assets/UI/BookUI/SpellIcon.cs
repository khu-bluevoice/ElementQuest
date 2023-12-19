using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour
{
    public Image image;

    [SerializeField]
    Image disabledImage;

    public TextMeshProUGUI spellName;
    public bool showName = true;

    public void Disable()
    {
        image.gameObject.SetActive(false);
        disabledImage.gameObject.SetActive(true);
        spellName.text = "?";
    }

    public void HideName()
    {
        spellName.gameObject.SetActive(false);
    }

    private void Start()
    {
        spellName.gameObject.SetActive(showName);
    }
}
