using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ProvinceUI : MonoBehaviour, IGameUI {
    VisualElement _provinceContainer;
    VisualElement _constructContainer;
    Label _provinceName;
    Label _barracksLevelLabel;
    Label _timeLeftLabel;
    Province _selectedProvince;
    Coroutine _updatePanelCoroutine;


    void Start() {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        _provinceContainer = rootVisualElement.Q<VisualElement>("provinceContainer");
        _constructContainer = rootVisualElement.Q<VisualElement>("constructContainer");
        _provinceName = rootVisualElement.Q<Label>("provinceName");
        _barracksLevelLabel = rootVisualElement.Q<Label>("barracksLevel");
        _timeLeftLabel = rootVisualElement.Q<Label>("timeLeft");
        _provinceContainer.Q<Button>("constructButton").clickable.clicked += ShowConstructPanel;
        _constructContainer.Q<Button>("barracksButton").clickable.clicked += () => AddBuilding(BuildingType.Barracks);
    }


    public void ShowPanel(ISelectable selectable) {
        _selectedProvince = (Province) selectable;
        ShowProvincePanel();
    }

    public void HidePanel() {
        HideProvincePanel();
    }

    public bool IsWaitingForInput() {
        return false;
    }

    /**
     *
     */
    void ShowProvincePanel() {
        Building selectedProvinceBuilding = _selectedProvince.Buildings[BuildingType.Barracks];
        _provinceName.text = _selectedProvince.provinceName;
        _barracksLevelLabel.text = selectedProvinceBuilding.BuildingLevel.ToString();
        _provinceContainer.style.display = DisplayStyle.Flex;
        _updatePanelCoroutine = StartCoroutine(UpdatePanel(selectedProvinceBuilding));
    }

    /**
     *
     */
    void HideProvincePanel() {
        _provinceContainer.style.display = DisplayStyle.None;
        if (_updatePanelCoroutine != null) {
            StopCoroutine(_updatePanelCoroutine);
        }
    }

    /**
     *
     */
    void ShowConstructPanel() {
        _constructContainer.style.display = DisplayStyle.Flex;
        HidePanel();
    }

    /**
     *
     */
    void HideConstructPanel() {
        _constructContainer.style.display = DisplayStyle.None;
    }

    /**
     *
     */
    void AddBuilding(BuildingType buildingType) {
        Building building = _selectedProvince.Buildings[buildingType];
        building.StartConstruction();
        StartCoroutine(building.UpdateConstruction());
        HideConstructPanel();
        ShowProvincePanel();
    }

    /**
     *
     */
    IEnumerator UpdatePanel(Building building) {
        while (true) {
            if (building.BeginConstructionTime == null) {
                _timeLeftLabel.text = "No Construction";
            } else {
                float timeLeft = building.ConstructionTime - building.ConstructionTimeLeft;
                TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
                _timeLeftLabel.text = $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            }
            _barracksLevelLabel.text = building.BuildingLevel.ToString();
            yield return new WaitForSeconds(1);
        }
    }
}