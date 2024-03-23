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

    // Yan Scriptlerden Veri Al��veri�i
    public CameraController camController;
    public OrbitController OrbController;

    // G�steri Sonras� Tu�lar ve Ba�l�k
    public GameObject Title;
    public GameObject StartButton;
    public GameObject ExitButton;

    // Baz� ayarlar i�in gerekli
    public OrbitAroundObject orbAround;

    private int i = 0;

    // Oyun ba�lang�c� i�in kontrol
    public bool isStarted = false;

    // Yard�m Tu�u ��in Gerekli
    public GameObject help;
    public GameObject firstHelpKey;
    public bool isHelpActive = false;

    // Yeniden ba�latma , Yard�m Tu�u ve 
    private void Update()
    {
        // Reset
        if (Input.GetKeyDown(KeyCode.R))
        {
            // 0 = Oyun Sahnesi
            SceneManager.LoadScene(0);
            OrbController.gameLevelReset();
        }

        // Yard�m Men�s�
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

        // Oyun i�indeyken ��k��
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

    // Oyundan ��k�� Fonksiyonu
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
     * Bekletme S�resi Ayarlamak ��in, �htiya� halinde kullan�labilir.
    IEnumerator WaitTime(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
    }
    */

}

