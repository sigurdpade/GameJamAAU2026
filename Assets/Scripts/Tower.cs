using UnityEngine;

[CreateAssetMenu(menuName = "TowerGame/Tower")]
public class Tower : ScriptableObject
{
    public string towerName;
    public Sprite towerSprite;
    public GameObject towerObject1;
    public GameObject towerObject2;
    public GameObject towerObject3;
    public int cost;

    public LearningInformation learningInformation;
}
