using Cinemachine;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject hero = GameObject.Find("hero");
        GetComponent<CinemachineVirtualCamera>().Follow = hero.transform;
    }
}
