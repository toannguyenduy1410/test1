using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObj/leveldata")]

public class LevelSO : ScriptableObject
{
   public List<LevelItemData> listlevelData;
}
