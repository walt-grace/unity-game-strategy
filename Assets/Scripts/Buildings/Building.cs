public enum BuildingType {
    Barracks,
    TankFactory,
}
public abstract class Building {
    public abstract string GetName();
    public abstract BuildingType GetBuildingType();
}