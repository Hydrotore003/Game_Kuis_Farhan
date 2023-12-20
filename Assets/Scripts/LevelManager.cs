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

   [SerializeField]
   private PemanggilSuara _pemanggilSuara = null;

   [SerializeField]
   private AudioClip _suaraMenang = null;

   [SerializeField]
   private AudioClip _suaraKalah = null;

   //[SerializeField]
   //private UI_Pertanyaan _nomorSoal = null;

   private int _indexSoal = 0;

   [SerializeField]
   private PlayerProgress _playerProgress = null;

   private void Start()
   {      
        //_playerProgress.SimpanProgress();

        _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;

        NextLevel();
        AudioManager.instance.PlayBGM(1);

        // Subscribe events
        UI_PoinJawaban.EventJawabanSoal += UI_PoinJawaban_EventJawabSoal;
   }

   private void OnDestroy()
   {
        UI_PoinJawaban.EventJawabanSoal -= UI_PoinJawaban_EventJawabSoal;
   }

   private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
   {
     _pemanggilSuara.PanggilSuara(adalahBenar ? _suaraMenang : _suaraKalah);

     if (!adalahBenar) return;

     string namaLevelPack = _inisialData.levelPack.name;
     int levelTerakhir = _playerProgress.progressData.progressLevel[namaLevelPack];

     if (_indexSoal + 2 > levelTerakhir)
     {
          // tambahkan koin sebagai hadiah menyelesaikan koin
          _playerProgress.progressData.koin += 20;

          // Membuka level selanjutnya agar dapat diakses di menu level
          _playerProgress.progressData.progressLevel[namaLevelPack] = _indexSoal + 2;

          _playerProgress.SimpanProgress();

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
