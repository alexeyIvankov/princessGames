using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Scenes
{
    public class ShowRoom : MonoBehaviour
    {
        public Transform characterTransform;
        public List<GameObject> prefabs;
        public GameObject character;
        private Vector2 initialPos;
        private Vector2 delatPos;
        public Boolean isUpToDate;
        public CharacterList characterList;
        public int currentCharacter;
        public TextMeshProUGUI characterName;

        private Boolean visible;
        // Start is called before the first frame update

       
        void Start()
        {
                //Getting a list with characters
                characterList = new CharacterList();
                for (int i = 0; i < characterList.GetLength(); i++)
                {
                    //Create list that contains prefabs of characters
                    prefabs.Add(Resources.Load<GameObject>("Prefabs/" + characterList.GetCharacter(i).prefabName));
                }

                currentCharacter = FindObjectOfType<AppPreferences>().GetCurrentCharacter();
                // Debug.Log(currentCharacter);
                // Debug.Log(FindObjectOfType<Selector>().ToString());
                // Debug.Log(currentCharacter.ToString());
                //Writing the name of current character
                characterName.text = characterList.GetCharacter(currentCharacter).characterName;
                // prefab = Resources.Load<GameObject>("Prefabs/" + characterList.GetCharacter(currentCharacter).prefabName);
                //Drawing a character
                character = Instantiate(prefabs[currentCharacter], characterTransform.position,
                    characterTransform.rotation);
                character.transform.RotateAround(characterTransform.position, Vector3.up, 180f);
                isUpToDate = true;
            


        }

        // Update is called once per frame
        void Update()
        {
          

                //Touch screen swipe control
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            initialPos = touch.position;
                            break;
                        case TouchPhase.Moved:
                            delatPos = initialPos - touch.position;
                            Debug.Log("Delta Position " + delatPos.x);
                            if (delatPos.x < -100f)
                            {
                                Debug.Log("Right");
                                //Rotate Right
                                character.transform.RotateAround(characterTransform.position, Vector3.up, -10f);
                            }
                            else if (delatPos.x > 100f)
                            {
                                Debug.Log("Left");
                                //Rotate Left
                                character.transform.RotateAround(characterTransform.position, Vector3.up, 10f);
                            }

                            break;
                    }




                }

                if (!isUpToDate)
                {
                    //Update character and character name when new character is chosen
                    character.SetActive(false);
                    //prefabs = Resources.Load<GameObject>("Prefabs/" + characterList.GetCharacter(currentCharacter).prefabName);
                    characterName.text = characterList.GetCharacter(currentCharacter).characterName;
                    character = Instantiate(prefabs[currentCharacter], characterTransform.position,
                        characterTransform.rotation);
                    character.transform.RotateAround(characterTransform.position, Vector3.up, 180f);
                    FindObjectOfType<AppPreferences>().SetCurrentCharacter(currentCharacter);
                    isUpToDate = true;
                }


        }
    }

}

