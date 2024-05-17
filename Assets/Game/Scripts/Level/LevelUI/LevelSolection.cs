using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class LevelSolection : MonoBehaviour
{
    [SerializeField] private LevelItemUI levelPrifab;
    [SerializeField] private Transform prantPosition;
    [SerializeField] private LevelSO levelData;
    [SerializeField] private GameObject Player;
    
    private GameObject currentMap; // lưu map hiện tại
    public GameObject currentPlayer; // lưu Player hiện tại

    public static LevelSolection instance;
    public string curentIndex;
    public Action ActionCamraFoloww;
   
    private void Awake()
    {
        instance = this;                    
    }

    private void Start()
    {
        LoadLevel();
    }
    private void SpawnLevel(Sprite imagelevel, string textlevel)
    {
        LevelItemUI levelItemIU = Instantiate(levelPrifab, prantPosition);
        levelItemIU.OnInit(imagelevel, textlevel, levelActionHand);
    }

    public void levelActionHand(string index)
    {
        curentIndex = index;
        GameObject mapfrifab = Resources.Load<GameObject>($"{Level_key.key_level}{index}");
        GameObject newMap = Instantiate(mapfrifab);
        GameObject newPlayer = Instantiate(Player);

        // Kiểm tra nếu có đối tượng map hiện tại, thì hủy nó trước khi tạo đối tượng mới
        if (currentMap != null)
        {
            Destroy(currentMap);
            Destroy(currentPlayer);
        }
        currentMap = newMap;
        currentPlayer = newPlayer;
       
        Invoke(nameof(InvokeActionDelay), 0.2f);
    }   
    private void InvokeActionDelay()
    {
        ActionCamraFoloww.Invoke();
        gameObject.SetActive(false);
       
    }
    private void LoadLevel()
    {
        for (int i = 0; levelData.listlevelData.Count > i; i++)
        {
            SpawnLevel(levelData.listlevelData[i].levelSprite, levelData.listlevelData[i].levelIndex.ToString());
        }

    }
}
