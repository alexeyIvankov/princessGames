using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dom : MonoBehaviour

{
    [Header("Attributes")] public static float range = 3f;
    public Color hoverColor;
    private GameManager gameManager;
    private Renderer rend;
    [SerializeField] public SceneField sceneName;
    public Transform player;
    public GameObject dialogueButton;


    private Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        gameManager = GameManager.instance;
        //        startColor = rend.material.color;
    }

//    private void OnMouseEnter()
//    {
////        Debug.Log("POINTER IS HEREEEEE!!!!!!");
//        if (EventSystem.current.IsPointerOverGameObject())
//            return;
//        GetComponent<Renderer>().material.color = hoverColor;
//    }
//
//    private void OnMouseExit()
//    {
//        rend.material.color = startColor;
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireSphere(transform.position, range);
//    }

//    private void OnMouseDown()
//    {
//        gameManager.setNameScene(sceneName.SceneName);
//    }

    void Update()
    {
        // Vector3 dir = player.position - transform.position;
        // float distanceThisFrame = speed * Time.deltaTime;
        // Debug.Log("DISTANCE IN FRAME" + distanceThisFrame);
        //  Debug.Log("РАССТОЯНИЕ" + dir.magnitude);
//        if (dir.magnitude <= range)
//        {
//            gameManager.EnterInHouse();
//            gameManager.setNameScene(sceneName.SceneName);
//            return;
//        }

        if (gameManager.endCol)
        {
            Time.timeScale = 1;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            dialogueButton.SetActive(true);
            // Time.timeScale = 0;
           // gameManager.EnterInHouse();
            gameManager.setNameScene(sceneName.SceneName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
       // gameManager.ExitHouse1();
        dialogueButton.SetActive(false);
    }
}