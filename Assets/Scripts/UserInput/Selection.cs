using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ISelectable {
    void ApplySelection();
    void RemoveSelection();
    UIType GetUIType();
}

internal interface IGameUI {
    void ShowPanel(ISelectable selectable);
    void HidePanel();
    bool IsWaitingForInput();
}

public class Selection : MonoBehaviour {
    GameObject _selected;
    IGameUI _currentUI;
    ArmyUI _armyUI;
    ProvinceUI _provinceUI;
    Camera _camera;
    public readonly Dictionary<int, ISelectable> Selectables = new();

    void Start() {
        _camera = Camera.main;
        _provinceUI = GetComponentInChildren<ProvinceUI>();
        _armyUI = GetComponentInChildren<ArmyUI>();
    }

    void Update() {
        if (IsSelectable()) {
            SelectObject();
        }
    }


    /**
     *
     */
    bool IsSelectable() {
        return Input.GetMouseButtonDown(0) &&
               !EventSystem.current.IsPointerOverGameObject() &&
               (_currentUI == null || !_currentUI.IsWaitingForInput());
    }

    /**
     *
     */
    void SelectObject() {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Collider2D hitCollider = Physics2D.Raycast(ray.origin, ray.direction).collider;
        if (!hitCollider) return;
        GameObject hitGameObject = hitCollider.gameObject;
        if (!_selected) {
            ISelectable selectable = Selectables[hitGameObject.GetInstanceID()];
            ShowUI(selectable);
            selectable.ApplySelection();
            _selected = hitGameObject;
        } else if (_selected != hitGameObject) {
            ISelectable newSelectable = Selectables[hitGameObject.GetInstanceID()];
            ISelectable oldSelectable = Selectables[_selected.GetInstanceID()];
            _selected = hitGameObject;
            HideUI();
            oldSelectable.RemoveSelection();
            ShowUI(newSelectable);
            newSelectable.ApplySelection();
        }
    }

    /**
     *
     */
    void ShowUI(ISelectable selectable) {
        _currentUI = selectable.GetUIType() switch {
            UIType.Province => _provinceUI,
            UIType.ArmyUnit => _armyUI,
            _ => null
        };
        _currentUI?.ShowPanel(selectable);
    }

    /**
     *
     */
    void HideUI() {
        _currentUI?.HidePanel();
    }
}