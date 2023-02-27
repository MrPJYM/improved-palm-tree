using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventory;
    public GameObject equipStore;
    public GameObject potionStore;
    public GameObject equipment;
    GameObject variableJoystick;
    void Start()
    {
        variableJoystick = GameObject.Find("Variable Joystick");
    }
    void Update()
    {
        if (Camera.main != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("eNPC"))
                    {
                        OpenEquipStore();
                    }
                    else if (hit.collider.CompareTag("pNPC"))
                    {
                        OpenPotionStore();
                    }
                }
            }
        }
    }
    public void OpenEquipStore()
    {
        equipStore.SetActive(true);
        inventory.SetActive(true);
        variableJoystick.SetActive(false);
    }
    public void CloseEquipStoreButton()
    {
        equipStore.SetActive(false);
        variableJoystick.SetActive(true);
    }
    public void OpenPotionStore()
    {
        potionStore.SetActive(true);
        inventory.SetActive(true);
        variableJoystick.SetActive(false);
    }
    public void ClosePotionStoreButton()
    {
        potionStore.SetActive(false);
        variableJoystick.SetActive(true);
    }
    public void OpenInventoryButton()
    {
        inventory.SetActive(true);
        equipment.SetActive(true);
    }
    public void CloseInventoryButton()
    {
        inventory.SetActive(false);
    }
    public void CloseEquipmentButton()
    {
        equipment.SetActive(false);
    }
}