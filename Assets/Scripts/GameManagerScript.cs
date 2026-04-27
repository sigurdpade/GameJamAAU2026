using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript main;

    public Transform startPoint;
    public Transform[] path;

    public void Awake()
    {
        main = this;
    }
}
