using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Pertanyaan : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatTeks = null;

    [SerializeField]
    private TextMeshProUGUI _tempatJudulLevel = null;

    [SerializeField]
    private Image _tempatGambar = null;

    void Start()
    {
        //Debug.Log("Isi tempat teks yaitu:");
        //Debug.Log(_tempatTeks.text);
    }

    public void SetPertanyaan(string teksPertanyaan, Sprite gambarHint)
    {
        _tempatTeks.text = teksPertanyaan;
        _tempatGambar.sprite = gambarHint;
    }

    public void SetSoal(string teksJudulLevel)
    {
        _tempatJudulLevel.text = teksJudulLevel;
    }
}
