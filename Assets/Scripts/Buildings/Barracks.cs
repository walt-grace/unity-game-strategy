public class Barracks : Building {
    public override string GetName() {
        return "Barracks";
    }

    public override BuildingType GetBuildingType() {
        return BuildingType.Barracks;
    }
}