using UnityEngine;
using UnityEngine.EventSystems;

public class Popup : MonoBehaviour

{
    public Transform cam1; // Камера персонажа из которой будет выходить луч
    RaycastHit rch1; // Собственно сам луч
   // public GameObject point; // Точка на второй сцене в которой буде появляться персона
    private GameManager _gameManager;


    
    private void Start()
    {
        _gameManager = GameManager.instance;
    }

    
    void Update()
    {
       // Debug.Log("SCENE" + _gameManager.getNameScene());
        if (_gameManager.getNameScene() != null)
        {
            Vector3 Direction = cam1.TransformDirection(Vector3.forward);
            if (Physics.Raycast(cam1.position, Direction, out rch1, 3))
            {
                // Луч будет выходить из камеры на расстоянии 3 метра
                if (rch1.collider.GetComponent<Bot1>())
                {
                    _gameManager.visible = true;
                }
            }
        }
    }

    void OnGUI()
    {
        //Создадим диалог
        if (_gameManager.visible)
        {
            // если visible = true
            _gameManager.EnterInHouse();
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}