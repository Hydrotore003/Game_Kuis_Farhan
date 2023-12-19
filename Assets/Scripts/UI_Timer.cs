using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Timer : MonoBehaviour
{
    public static event System.Action EventWaktuHabis;

    [SerializeField]
    private Slider _timeBar = null;

    [SerializeField]
    private float _waktujawab = 30; // Dalam Detik

    private bool _waktuBerjalan = false;

    private float _sisaWaktu = 0;
    
    public bool WaktuBerjalan
    {
        get => _waktuBerjalan;
        set => _waktuBerjalan = value;
    }    

    private void Start()
    {
        Ulangiwaktu();
        _waktuBerjalan = true;
    }

    // [SerializeField]
    // private UI_PesanLevel _tempatPesan = null;

    private void Update()
    {
        if(!_waktuBerjalan)
        return;

        // Dikurangi sebanyak waktu menggambar satu frame (Second per Frame)
        _sisaWaktu -= Time.deltaTime;
        _timeBar.value = _sisaWaktu / _waktujawab;

        if (_sisaWaktu <= 0f)
        {
            // _tempatPesan.Pesan = "Waktu Habis";
            // _tempatPesan.gameObject.SetActive(true);
            //Debug.Log("Waktu Habis");

            EventWaktuHabis?.Invoke();
            _waktuBerjalan = false;
            return;
        }

        //Debug.Log(_sisaWaktu);
    }

    public void Ulangiwaktu()
    {
        _sisaWaktu = _waktujawab;
    }

}
