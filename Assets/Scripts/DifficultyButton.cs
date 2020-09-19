using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
  private Button button;
  private GameObject player;

  public int difficulty;
  // Start is called before the first frame update
  void Start()
  {
      button = GetComponent<Button>();
      player = GameObject.Find("Player");
      button.onClick.AddListener(SetDifficulty);
  }

  // Update is called once per frame
  void Update()
  {

  }

  void SetDifficulty()
  {
      //Debug.Log(button.gameObject.name + " was clicked");
      player.GetComponent<PlayerController>().StartGame(difficulty);
  }
}
