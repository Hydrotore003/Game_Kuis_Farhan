using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Level Pack Baru",
    menuName = "Game Kuis/Level Pack"
)]
public class LevelPackKuis : ScriptableObject
{
    [SerializeField]
    private LevelSoalKuis[] _isiLevel = new LevelSoalKuis[0];

    public int BanyakLevel => _isiLevel.Length;

    public LevelSoalKuis AmbilLevelKe(int index)
    {
        return _isiLevel[index];
    }
}
