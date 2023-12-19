using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
   [System.Serializable]
   private struct DataSoal
   {
        public Sprite hint;
        public string pertanyaan;

        public string[] jawabanTeks;
        public bool[] adalahbenar;
   }

   [SerializeField]
   private InisialDataGameplay _inisialData = null;

   [SerializeField]
   private UI_Pertanyaan _tempatPertanyaan = null;

   [SerializeField]
   private UI_PoinJawaban[] _tempatPilihanJawaban = new UI_PoinJawaban[0];

   [SerializeField]
   private GameSceneManager _gameSceneManager = null;

   [SerializeField]
   private string _namaScenePilihMenu = string.Empty;

   //[SerializeField]
   //private UI_Pertanyaan _nomorSoal = null;

   private int _indexSoal = 0;

   [SerializeField]
   private PlayerProgress _playerProgress = null;

   private void Start()
   {
         // Check juka tidak berhasil memuat progress
         if (!_playerProgress.MuatProgress())
         {
           // Buat simpanan progress atau ganti dengan yang baru
          _playerProgress.SimpanProgress();
         }
        
        //_playerProgress.SimpanProgress();

        _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;

        NextLevel();

        // Subscribe events
        UI_PoinJawaban.EventJawabanSoal += UI_PoinJawaban_EventJawabSoal;
   }

   private void OnDestroy()
   {
        UI_PoinJawaban.EventJawabanSoal -= UI_PoinJawaban_EventJawabSoal;
   }

   private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
   {
     if (adalahBenar)
     {
          _playerProgress.progressData.koin += 20;
     }
   }

   [SerializeField]
   private LevelPackKuis _soalSoal = null;
   public void NextLevel()
   { 
        //Soal index selanjutnya
        _indexSoal++;
        
        //Jika index melampaui soal terakhir, ulang dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel)
        {
            //_indexSoal = 0;
            _gameSceneManager.BukaScene(_namaScenePilihMenu);
            return;
        }

        //Ambil data pertanyaan
        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        //Set informasi soal
        _tempatPertanyaan.SetPertanyaan("Level " + (_indexSoal + 1), soal.pertanyaan, soal.hint);

        for (int i = 0; i < _tempatPilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _tempatPilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
   }

   private void OnApplicationQuit()
   {
     _inisialData.SaatKalah = false;
   }
}
