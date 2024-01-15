using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Province : MonoBehaviour, ISelectable {
    SpriteRenderer _sprite;
    public string provinceName;
    public Dictionary<BuildingType, Building> buildings = new();

    void Start() {
        _sprite = GetComponent<SpriteRenderer>();
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

    public void AddBuilding(Building building) {
        if (buildings.ContainsKey(building.GetBuildingType())) {

        }
    }
}