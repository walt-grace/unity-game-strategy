using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour, ISelectable {
    SpriteRenderer _sprite;
    public string provinceName;
    public readonly Dictionary<BuildingType, Building> Buildings = new();

    void Start() {
        _sprite = GetComponent<SpriteRenderer>();
        SetupBuilding();
    }

    public void ApplySelection() {
        _sprite.color = Color.red;
    }

    public void RemoveSelection() {
        _sprite.color = Color.white;
    }

    public UIType GetUIType() {
        return UIType.Province;
    }

    /**
     *
     */
    void SetupBuilding() {
        Buildings[BuildingType.Barracks] = new Barracks();
    }
}