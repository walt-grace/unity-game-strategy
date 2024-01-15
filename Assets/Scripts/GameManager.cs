using UnityEngine;

public class GameManager : MonoBehaviour {
    public Selection selection;
    public GameObject provinces;
    public GameObject units;
    public Province provincePrefab;
    public ArmyUnit armyUnitPrefab;

    void Start() {
        DrawMap();
        SetupUnits();
    }

    /**
     *
     */
    void DrawMap() {
        const int dims = 10;
        float cellSize = provincePrefab.GetComponent<SpriteRenderer>().localBounds.size.x;
        for (int i = -dims; i < dims; i++) {
            for (int j = -dims; j < dims; j++) {
                Vector3 tilePosition = new(i * cellSize, j * cellSize, 0f);
                Province province = Instantiate(provincePrefab, tilePosition, Quaternion.identity, provinces.transform);
                province.provinceName = "Jerusalem" + i + j;
                selection.Selectables[province.gameObject.GetInstanceID()] = province;
            }
        }
    }

    /**
     *
     */
    void SetupUnits() {
        ArmyUnit armyUnit = Instantiate(armyUnitPrefab, Vector3.zero, Quaternion.identity, units.transform);
        selection.Selectables[armyUnit.gameObject.GetInstanceID()] = armyUnit;
    }
}
