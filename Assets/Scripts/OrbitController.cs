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
    // Ana de�i�kenler
    public GameObject movingObjectPrefab; // Haraket eden objenin prefab�
    public Transform centerObject; // Sabit obje
    public int radius = 2; // D�nme yar��ap�
    public int speed = 2; // D�nme h�z�

    // Line Renderer ve de�i�kenleri
    private Vector3 axis = Vector3.up; 
    private GameObject movingObjectInstance; 
    private LineRenderer lineRenderer;
    public float lineRendererStartWitdh;    
    public float lineRendererEndWidth;      

    // Kullan�c�dan Yar��ap De�erini Almak i�in
    public InputField radVal;

    // Kullan�c�dan H�z De�erini Almak i�in
    public InputField speVal;           

    // Ayarlar Men�s�
    public GameObject SettingsMenu;

    // Ayarlar men�s�n� kontrol eden de�i�ken
    public bool isSettingActive = false;    

    // Bildirim
    public GameObject Notif;         
    
    // Bildirim ��in De�i�ken
    public Text NotifTextGame;

    // Y�r�nge �izmek ��in Gerekli
    public OrbitAroundObject orbLine;

    // Kamera H�z� Ayarlamak ��in Gerekli 
    public CameraController camController;

    // Oyun ba�lan�g�c� kontrol i�in
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
            NotifTextGame.text = "S�f�rdan Farkl� ve Pozitif De�er Girin";
            StartCoroutine(WaitTime(2f));
            Notif.SetActive(false);
            NotifTextGame.text = "";
        }
        else if (newRadValue <= 0 || newSpeedValue <= 0)
        {
            Notif.SetActive(true);
            NotifTextGame.text = "S�f�rdan Farkl� ve Pozitif De�er Girin";
            StartCoroutine(WaitTime(2f));
            Notif.SetActive(false);
            NotifTextGame.text = "";
        }
        else if (newRadValue <= 0)
        {
            Notif.SetActive(true);
            NotifTextGame.text = "Yar��ap S�f�r ya da S�f�rdan K���k Olamaz";
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
        NotifTextGame.text = "Ayarlar Ba�ar�yla Uyguland� !!";
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
        NotifTextGame.text = "Ayarlar S�f�rland� !!";
        yield return new WaitForSeconds(0.95f);
        NotifTextGame.text = "Yar��ap : "+radius+" // "+"H�z : "+speed;
        yield return new WaitForSeconds(2f);
        Notif.SetActive(false);
        NotifTextGame.text = "";
    }

    IEnumerator ResetGame()
    {
        Notif.SetActive(true);
        NotifTextGame.text = "Sahne Ba�ar�yla S�f�rland�";
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
