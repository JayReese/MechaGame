using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthGUI : MonoBehaviour {


    [SerializeField]
    GameObject HealthCanvas;
    [SerializeField]
    Image HealthBar;
    [SerializeField]
    Image BackgroundBar;
    [SerializeField]
    Player player;
    float playerMaxHealth;

	// Use this for initialization
	void Start () {
        HealthCanvas = transform.FindChild("Health").gameObject;
        player = FindObjectOfType<Player>();
        HealthBar = HealthCanvas.transform.GetChild(1).GetComponent<Image>();
        BackgroundBar = HealthCanvas.transform.GetChild(0).GetComponent<Image>();
        playerMaxHealth = player.Health;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            player.Health -= 2;
        }

        HealthBar.fillAmount = player.Health / playerMaxHealth;   
        
    }

}
