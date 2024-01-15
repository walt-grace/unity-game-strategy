using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ProvinceUI : MonoBehaviour, IGameUI {
    VisualElement _provinceContainer;
    VisualElement _constructContainer;
    Label _provinceName;
    Label _barracksLevel;
    Label _timeLeftLabel;
    Province _selectedProvince;


    void Start() {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        _provinceContainer = rootVisualElement.Q<VisualElement>("provinceContainer");
        _constructContainer = rootVisualElement.Q<VisualElement>("constructContainer");
        _provinceName = rootVisualElement.Q<Label>("provinceName");
        _barracksLevel = rootVisualElement.Q<Label>("barracksLevel");
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
        _barracksLevel.text = selectedProvinceBuilding.BuildingLevel.ToString();
        _provinceContainer.style.display = DisplayStyle.Flex;
        StartCoroutine(selectedProvinceBuilding.GetTimeLeft(_timeLeftLabel));
    }

    /**
     *
     */
    void HideProvincePanel() {
        _provinceContainer.style.display = DisplayStyle.None;
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
        _selectedProvince.AddBuilding(buildingType);
        HideConstructPanel();
        ShowProvincePanel();
    }
}