using UnityEngine;

[CreateAssetMenu(menuName = "TowerGame/Tower")]
public class Tower : ScriptableObject
{
    public string towerName;
    public Sprite towerSprite;
    public GameObject towerObject;
    public int cost;
}
