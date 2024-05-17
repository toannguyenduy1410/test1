using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonChoiLai;
    [SerializeField] private Button buttonComBackMenu;
    [SerializeField] private GameObject ObjPlay;
    [SerializeField] private GameObject ObjMenuLevel;
    [SerializeField] private GameObject ObjLoss;

    public bool Playing = false;
    //private bool Win = false;
   // public bool Loss = false;
    private bool Nons = true;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        buttonPlay.onClick.AddListener(GamPlay);
        buttonChoiLai.onClick.AddListener(ChoiLai);
        buttonComBackMenu.onClick.AddListener(ComBackMenu);
        
    }   
    private void Update()
    {
        SetupPlayerActions();
        
    }
    private void SetupPlayerActions()
    {
        if (player.Instance != null)
        {
            player.Instance.WinAction -= TimeLevelOn;
            player.Instance.WinAction += TimeLevelOn;
            player.Instance.LossAction -= LossGame;
            player.Instance.LossAction += LossGame;
        }
    }
    private void GamPlay()
    {
        if (Nons == true)
        {
            Nons = false;
            Playing = true;
            ObjPlay.SetActive(false);
        }
    }
    private void TimeLevelOn()
    {
        Invoke(nameof(MenuLevel), 3f);
    }
    private void MenuLevel()
    {
        if (player.Instance.isWin == true)
        {
            ObjMenuLevel.SetActive(true);
        }
    }
    private void LossGame()
    {                        
            ObjLoss.SetActive(true);       
    }
    private void ChoiLai()
    {
        LevelSolection.instance.levelActionHand(LevelSolection.instance.curentIndex);
        ObjLoss.SetActive(false);
    }
    private void ComBackMenu()
    {              
        player.Instance.isloss = true;
        ObjLoss.SetActive(false);
        ObjMenuLevel.SetActive(true);

    }
}
