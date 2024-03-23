/*                                                                                                                                                                                                  
                                                                                            *********************
                                                                                            *     QuartzzDev    *
                                                                                            ********************* 
*/                                                                                                                                                                                                  

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Diagnostics;
using JetBrains.Annotations;

public class OrbitController : MonoBehaviour
{
    // Ana deðiþkenler
    public GameObject movingObjectPrefab; // Haraket eden objenin prefabý
    public Transform centerObject; // Sabit obje
    public int radius = 2; // Dönme yarýçapý
    public int speed = 2; // Dönme hýzý

    // Line Renderer ve deðiþkenleri
    private Vector3 axis = Vector3.up; 
    private GameObject movingObjectInstance; 
    private LineRenderer lineRenderer;
    public float lineRendererStartWitdh;    
    public float lineRendererEndWidth;      

    // Kullanýcýdan Yarýçap Deðerini Almak için
    public InputField radVal;

    // Kullanýcýdan Hýz Deðerini Almak için
    public InputField speVal;           

    // Ayarlar Menüsü
    public GameObject SettingsMenu;

    // Ayarlar menüsünü kontrol eden deðiþken
    public bool isSettingActive = false;    

    // Bildirim
    public GameObject Notif;         
    
    // Bildirim Ýçin Deðiþken
    public Text NotifTextGame;

    // Yörünge Çizmek Ýçin Gerekli
    public OrbitAroundObject orbLine;

    // Kamera Hýzý Ayarlamak Ýçin Gerekli 
    public CameraController camController;

    // Oyun baþlanýgýcý kontrol için
    public GameManager gameManager;



    void Start()
    {
        SpawnMovingObject();

        lineRenderer = movingObjectInstance.AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = lineRendererStartWitdh;
        lineRenderer.endWidth = lineRendererEndWidth;
    }

    void Update()
    {
        if (movingObjectInstance != null)
        {
            Vector3 offset = movingObjectInstance.transform.position - centerObject.position;
            Vector3 circularMotion = Quaternion.AngleAxis(speed * Time.deltaTime, axis) * offset;
            movingObjectInstance.transform.position = centerObject.position + circularMotion.normalized * radius;

            UpdateLineRendererPositions();
        }

        if (Input.GetKeyDown(KeyCode.E) && isSettingActive == false && gameManager.isStarted)
        {
            SettingsMenu.SetActive(true);
            isSettingActive = true;

        }
        else if (Input.GetKeyDown(KeyCode.E) && isSettingActive && gameManager.isStarted)
        {
            SettingsMenu.SetActive(false);
            isSettingActive = false;
        }


    }

    public void Apply()
    {
        int newRadValue = int.Parse(radVal.text);
        int newSpeedValue = int.Parse(speVal.text);

        if (newRadValue <= 0 && newSpeedValue <= 0) 
        {
            Notif.SetActive(true);
            NotifTextGame.text = "Sýfýrdan Farklý ve Pozitif Deðer Girin";
            StartCoroutine(WaitTime(2f));
            Notif.SetActive(false);
            NotifTextGame.text = "";
        }
        else if (newRadValue <= 0 || newSpeedValue <= 0)
        {
            Notif.SetActive(true);
            NotifTextGame.text = "Sýfýrdan Farklý ve Pozitif Deðer Girin";
            StartCoroutine(WaitTime(2f));
            Notif.SetActive(false);
            NotifTextGame.text = "";
        }
        else if (newRadValue <= 0)
        {
            Notif.SetActive(true);
            NotifTextGame.text = "Yarýçap Sýfýr ya da Sýfýrdan Küçük Olamaz";
            StartCoroutine(WaitTime(2f));
            Notif.SetActive(false);
            NotifTextGame.text = "";
        }
        else
        {
            radius = newRadValue;
            speed = newSpeedValue;
            StartCoroutine(NotifScreenForApply());
        }


    }

    public void Reset()
    {
        radius = 5;
        speed = 350;
        StartCoroutine(NotifScreenForReset());
    }

    IEnumerator NotifScreenForApply()
    {
        Notif.SetActive(true);
        NotifTextGame.text = "Ayarlar Baþarýyla Uygulandý !!";
        yield return new WaitForSeconds(1.55f);
        Notif.SetActive(false);
        NotifTextGame.text = "";

    }

    IEnumerator WaitTime(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
    }


    IEnumerator NotifScreenForReset()
    {
        Notif.SetActive(true);
        NotifTextGame.text = "Ayarlar Sýfýrlandý !!";
        yield return new WaitForSeconds(0.95f);
        NotifTextGame.text = "Yarýçap : "+radius+" // "+"Hýz : "+speed;
        yield return new WaitForSeconds(2f);
        Notif.SetActive(false);
        NotifTextGame.text = "";
    }

    IEnumerator ResetGame()
    {
        Notif.SetActive(true);
        NotifTextGame.text = "Sahne Baþarýyla Sýfýrlandý";
        yield return new WaitForSeconds(2f);
        Notif.SetActive(false);
        NotifTextGame.text = "";
    }

    public void gameLevelReset()
    {
        StartCoroutine(ResetGame());
    }

    void UpdateLineRendererPositions()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, centerObject.position);
        lineRenderer.SetPosition(1, movingObjectInstance.transform.position);
    }

    void SpawnMovingObject()
    {
        if (movingObjectPrefab != null)
        {
            movingObjectInstance = Instantiate(movingObjectPrefab, centerObject.position + Vector3.right * radius, Quaternion.identity);
        }
        else
        {
            // Nothing
        }
    }
}
