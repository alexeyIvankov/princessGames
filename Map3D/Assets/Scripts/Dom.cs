using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dom : MonoBehaviour

{

    [Header("Attributes")] public float range = 3f;
    public Color hoverColor;
    private GameManager gameManager;
    private Renderer rend;
    [SerializeField]
    public SceneField sceneName;

   

    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        gameManager = GameManager.instance;
        //        startColor = rend.material.color;
    }

    private void OnMouseEnter()
    {
//        Debug.Log("POINTER IS HEREEEEE!!!!!!");
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        GetComponent<Renderer>().material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnMouseDown()
    {
        gameManager.setNameScene(sceneName.SceneName);
    }
}