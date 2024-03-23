/*                                                                                                                                                                                                  
                                                                                            *********************
                                                                                            *     QuartzzDev    *
                                                                                            ********************* 
*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class GameManager : MonoBehaviour
{
    // Kameralar
    public GameObject startCam;
    public GameObject gameCam;

    // Yan Scriptlerden Veri Alýþveriþi
    public CameraController camController;
    public OrbitController OrbController;

    // Gösteri Sonrasý Tuþlar ve Baþlýk
    public GameObject Title;
    public GameObject StartButton;
    public GameObject ExitButton;

    // Bazý ayarlar için gerekli
    public OrbitAroundObject orbAround;

    private int i = 0;

    // Oyun baþlangýcý için kontrol
    public bool isStarted = false;

    // Yardým Tuþu Ýçin Gerekli
    public GameObject help;
    public GameObject firstHelpKey;
    public bool isHelpActive = false;

    // Yeniden baþlatma , Yardým Tuþu ve 
    private void Update()
    {
        // Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 0 = Oyun Sahnesi
            SceneManager.LoadScene(0);
            OrbController.gameLevelReset();
        }

        // Yardým Menüsü
        if (Input.GetKeyDown(KeyCode.H) && !isHelpActive && isStarted)
        {
            firstHelpKey.SetActive(false);
            help.SetActive(true);
            isHelpActive = true;

        }
       else if (Input.GetKeyDown(KeyCode.H) && isHelpActive && isStarted)
        {
            firstHelpKey.SetActive(true);
            help.SetActive(false);
            isHelpActive = false;

        }

        // Oyun içindeyken Çýkýþ
        if (Input.GetKeyDown(KeyCode.M) && isStarted)
        {
            Application.Quit();
        }
    }

    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Basla Fonksiyonu
    public void Basla()
    {

        Title.SetActive(false);
        StartButton.SetActive(false);
        ExitButton.SetActive(false);

        camController.isStartOK = true;
        startCam.SetActive(false);
        gameCam.SetActive(true);
        isStarted = true;
        orbAround.enabled = true;
        OrbController.speed -= 300;
    }

    // Oyundan Çýkýþ Fonksiyonu
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        OrbController.radius = 5;
        OrbController.speed = 0;

        int MaxSpeed = 650;

        while (i == 0)
        {
            yield return new WaitForSeconds(0.2f);
            OrbController.speed += 50;

            if (OrbController.speed == MaxSpeed)
            {
                yield return new WaitForSeconds(0.1f);
                Title.SetActive(true);
                StartButton.SetActive(true);
                ExitButton.SetActive(true);
                i++;
            }
        }
    }

    
    /*
     * 
     * Bekletme Süresi Ayarlamak Ýçin, Ýhtiyaç halinde kullanýlabilir.
    IEnumerator WaitTime(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
    }
    */

}

