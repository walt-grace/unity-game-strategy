public enum BuildingType {
    Barracks,
    TankFactory,
}
public abstract class Building {
    public int BuildingLevel;
    public abstract BuildingType GetBuildingType { get; }

    /**
     *
     */
    public void UpdateBuilding() {
        BuildingLevel++;
    }
}