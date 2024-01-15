using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ArmyUI : MonoBehaviour, IGameUI {
    VisualElement _armyUnitContainer;
    Label _armyUnitName;
    ArmyUnit _selectedArmy;
    bool _waitForMoveInput;
    Camera _camera;

    void Start() {
        _camera = Camera.main;
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        _armyUnitContainer = rootVisualElement.Q<VisualElement>("armyUnitContainer");
        _armyUnitContainer.Q<Button>("moveButton").clickable.clicked += OnMoveButtonClick;
    }

    void Update() {
        if (IsWaitingForInput() && Input.GetMouseButtonDown(0)) {
            Vector3 targetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            StartCoroutine(_selectedArmy.MoveUnit(targetPosition));
            _waitForMoveInput = false;
        }
    }

    public void ShowPanel(ISelectable selectable) {
        _selectedArmy = (ArmyUnit) selectable;
        _armyUnitContainer.style.display = DisplayStyle.Flex;
    }

    public void HidePanel() {
        _armyUnitContainer.style.display = DisplayStyle.None;
    }

    public bool IsWaitingForInput() {
        return _waitForMoveInput;
    }

    /**
     *
     */
    void OnMoveButtonClick() {
        _waitForMoveInput = true;
    }
}