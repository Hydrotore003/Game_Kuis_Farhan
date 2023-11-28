using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_PesanLevel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatPesan = null;

    public string Pesan
    {
        get 
        {
            Debug.Log("Getter dijalankan");
            return _tempatPesan.text;
        }
        set
        {
            Debug.Log("Setter dijalankan");
            _tempatPesan.text = value;
        }
    }

    private void Awake()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}
