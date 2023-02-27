using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBuffer : MonoBehaviour
{
    public static ItemBuffer instance;
    private void Awake()
    {
        instance = this;
    }
    public List<ItemProperty> items = new List<ItemProperty>();
}
