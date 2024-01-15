using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ProvinceUI : MonoBehaviour, IGameUI {
    VisualElement _provinceContainer;
    VisualElement _constructContainer;
    Label _provinceName;
    ListView _buildingsView;
    Province _selectedProvince;



    void Start() {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        _provinceContainer = rootVisualElement.Q<VisualElement>("provinceContainer");
        _constructContainer = rootVisualElement.Q<VisualElement>("constructContainer");
        _provinceName = rootVisualElement.Q<Label>("provinceName");
        _provinceContainer.Q<Button>("constructButton").clickable.clicked += ShowConstructPanel;
        _constructContainer.Q<Button>("barracksButton").clickable.clicked += () => AddBuilding(new Barracks());
        _buildingsView = _provinceContainer.Q<ListView>("buildingsView");
    }

    /**
     *
     */
    public void ShowPanel(ISelectable selectable) {
        _selectedProvince = (Province) selectable;
        ShowProvincePanel();
    }

    /**
     *
     */
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
        _provinceName.text = _selectedProvince.provinceName;
        IList list = _selectedProvince.buildings.Values.Select(obj => obj.GetName()).ToList();
        _buildingsView.itemsSource = list;
        _provinceContainer.style.display = DisplayStyle.Flex;
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
    void AddBuilding(Building building) {
        _selectedProvince.AddBuilding(building);
        HideConstructPanel();
        ShowProvincePanel();
    }
}