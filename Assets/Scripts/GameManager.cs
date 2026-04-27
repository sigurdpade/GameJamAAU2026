using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public Transform startPoint;
    public Transform[] path;

    public void Awake()
    {
        main = this;
    }
}
