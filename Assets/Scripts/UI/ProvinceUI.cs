using UnityEngine;
using UnityEngine.UIElements;

public class ProvinceUI : MonoBehaviour, IGameUI {
    VisualElement _provinceContainer;
    VisualElement _constructContainer;
    Label _provinceName;
    Label _barracksLevel;
    Province _selectedProvince;


    void Start() {
        VisualElement rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        _provinceContainer = rootVisualElement.Q<VisualElement>("provinceContainer");
        _constructContainer = rootVisualElement.Q<VisualElement>("constructContainer");
        _provinceName = rootVisualElement.Q<Label>("provinceName");
        _barracksLevel = rootVisualElement.Q<Label>("barracksLevel");
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
        _provinceName.text = _selectedProvince.provinceName;
        _barracksLevel.text = _selectedProvince.Buildings[BuildingType.Barracks].BuildingLevel.ToString();
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
    void AddBuilding(BuildingType buildingType) {
        _selectedProvince.AddBuilding(buildingType);
        HideConstructPanel();
        ShowProvincePanel();
    }
}