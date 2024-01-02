using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnykeyPress : MonoBehaviour
{
    [SerializeField] private TMP_Text anykeyPress;

    void Update()
    {
        AnykeyPressGameStart();       
    }

    private void AnykeyPressGameStart()
    {
        if (!Input.anyKeyDown)
            return;

        SceneManager.LoadScene("InGame");
    }
}
