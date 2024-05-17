using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private Image imageLevel;
    [SerializeField] private Button LevelButton;

    public void OnInit(Sprite spriteLevel, string textLevel, Action<string> levelbuttonclick)
    {
        this.textLevel.text = textLevel;
        imageLevel.sprite = spriteLevel;
        LevelButton.onClick.AddListener(() =>
        {
            levelbuttonclick.Invoke(textLevel);

        });
    }
}
