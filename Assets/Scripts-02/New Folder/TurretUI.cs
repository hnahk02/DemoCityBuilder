using System;
using UnityEngine;
using UnityEngine.UI;



public class TurretUI : MonoBehaviour
{


    public Button wp1, wp2, wp3, wp4; 

    public Turret currentTurret;

    public Transform arrow, stone, canon, gun;


    private void Awake()
    {
        wp1.onClick.AddListener(() => {
            if(currentTurret != null){
                if(currentTurret.hasWeapon == false){
                    Weapon weapon = Instantiate(arrow, currentTurret.transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity, currentTurret.transform).GetComponent<Weapon>();
                    currentTurret.hasWeapon = true;
                    weapon.gameObject.SetActive(true);
                    HideTurretUI();

                }
            }
        });

         wp2.onClick.AddListener(() => {
            if(currentTurret != null){
                if(currentTurret.hasWeapon == false){
                    Weapon weapon = Instantiate(canon, currentTurret.transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity, currentTurret.transform).GetComponent<Weapon>();
                    currentTurret.hasWeapon = true;
                    weapon.gameObject.SetActive(true);
                    HideTurretUI();

                }
            }
        });

         wp3.onClick.AddListener(() => {
            if(currentTurret != null){
                if(currentTurret.hasWeapon == false){
                    Weapon weapon = Instantiate(stone, currentTurret.transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity, currentTurret.transform).GetComponent<Weapon>();
                    currentTurret.hasWeapon = true;
                    weapon.gameObject.SetActive(true);
                    HideTurretUI();

                }
            }
        });

         wp4.onClick.AddListener(() => {
            if(currentTurret != null){
                if(currentTurret.hasWeapon == false){
                    Weapon weapon = Instantiate(gun, currentTurret.transform.position + new Vector3(0f, 0.2f, 0f), Quaternion.identity, currentTurret.transform).GetComponent<Weapon>();
                    currentTurret.hasWeapon = true;
                    weapon.gameObject.SetActive(true);
                    HideTurretUI();
                }
            }
        });
    }

  

    private void OnEnable()
    {
        Turret.OnClickTurret += ShowTurretUI;
    }

    private void ShowTurretUI(object sender, EventArgs e)
    {
        currentTurret = sender as Turret;
        gameObject.SetActive(true);
    }

    private void HideTurretUI(){
        gameObject.SetActive(false);
        currentTurret = null;
        
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
