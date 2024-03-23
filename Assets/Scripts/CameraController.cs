/*                                                                                                                                                                                                  
                                                                                            *********************
                                                                                            *     QuartzzDev    *
                                                                                            ********************* 
*/


using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float hareketHizi = 5.0f;
    public float donmeHizi = 2.0f;
    public float yukariHareketHizi = 5.0f;
    public float asagiHareketHizi = 5.0f;

    public bool isStartOK = false;

    public OrbitController OrbitController;

    void Update()
    {
        HareketKontrol();
        DonmeKontrol();
        YukariAsagiHareketKontrol();
    }

    void HareketKontrol()
    {
        if (OrbitController.isSettingActive) {
        }
        else if (OrbitController.isSettingActive == false && isStartOK)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");

            Vector3 hareket = new Vector3(yatay, 0.0f, dikey) * hareketHizi * Time.deltaTime;
            transform.Translate(hareket);
        }
    }

    void DonmeKontrol()
    {
        if (OrbitController.isSettingActive) { 
        }
        else if (OrbitController.isSettingActive == false && isStartOK)
        {
            float mouseX = Input.GetAxis("Mouse X");

            Vector3 donme = new Vector3(0.0f, mouseX, 0.0f) * donmeHizi;
            transform.Rotate(donme);

            // Yatay dönüþ sýnýrlarýný ayarlamak için
            Vector3 currentRotation = transform.localRotation.eulerAngles;
            currentRotation.x = 0; // X ekseni (yatay) sýfýrlanýr.
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }

    void YukariAsagiHareketKontrol()
    {
        if (OrbitController.isSettingActive) { 
        }
        else if (OrbitController.isSettingActive == false && isStartOK)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Vector3 yukariHareket = Vector3.up * yukariHareketHizi * Time.deltaTime;
                transform.Translate(yukariHareket);
            }

            if (Input.GetKey(KeyCode.Z))
            {
                Vector3 asagiHareket = Vector3.down * asagiHareketHizi * Time.deltaTime;
                transform.Translate(asagiHareket);
            }
        }
    }
}
