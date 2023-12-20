using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private UI_LevelPackList _levelPackList = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    [SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    void Start()
    {
        // Check juka tidak berhasil memuat progress
         if (!_playerProgress.MuatProgress())
         {
           // Buat simpanan progress atau ganti dengan yang baru
          _playerProgress.SimpanProgress();
         }

        _levelPackList.LoadLevelPack(_levelPacks, _playerProgress.progressData);

        _tempatKoin.text = $"{_playerProgress.progressData.koin}";
        AudioManager.instance.PlayBGM(0);
    }
}
