using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UICarouselsSkins : MonoBehaviour
{
    [SerializeField] private UICarouselSkin itemPrefab;
    [SerializeField] private Transform itemsContent;

    private CarouselsSkinsManager gunSkinsManager;

    private List<UICarouselSkin> spawnedItems;

    //[Inject]
    private void Construct(CarouselsSkinsManager gunSkinsManager)
    {
        this.gunSkinsManager = gunSkinsManager;
        spawnedItems = new List<UICarouselSkin>();
        UpdatePanel();
    }

    private void OnEnable()
    {
        if (gunSkinsManager != null)
        {
            UpdatePanel();
        }
    }

    private void OnDestroy()
    {
        foreach (var item in spawnedItems)
        {
            item.onEquip -= OnSkinClick;
        }
    }

    private void UpdatePanel()
    {
        foreach (var item in spawnedItems)
        {
            item.onEquip -= OnSkinClick;
            Destroy(item.gameObject);
        }

        spawnedItems.Clear();

        for (int i = 0; i < gunSkinsManager.AllSkins.Length; i++)
        {
            CarouselInfo skinInfo = gunSkinsManager.AllSkins[i];
            UICarouselSkin newItem = Instantiate(itemPrefab, itemsContent);
            newItem.SetSkin(i, skinInfo == gunSkinsManager.CurrentSkin, skinInfo);
            newItem.onEquip += OnSkinClick;
            spawnedItems.Add(newItem);
        }
    }

    private void OnSkinClick(int index)
    {
        gunSkinsManager.EquipNew(index, true);
        UpdatePanel();
    }
}
