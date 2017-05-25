using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerScript : TestGameManager
{

    // Use this for initialization
    TestGameManager testGameManager;
    [SerializeField]
    Text playerAmmoText;
    [SerializeField]
    Text playerHealthText;
    float checkPointTextTime = 2.5f;
    //Variables private
    float m_HorizontalInputValue;
    float m_VerticalInputValue;
    int playerHealth = 100;
    bool isFiring = false;
    int playerAmmo = 1000;

    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplosionPrefab;
    public GameObject Smoke;

    public float m_CurrentHealth;
    private bool m_Dead;

    [SerializeField]
    GameObject ammoTextPanel;
    void Start()
    {
        TestGameManager testGameManager = GetComponent<TestGameManager>();
    }
    //Public variables go here.
   
    //public int m_PlayerNumber = 1;

    void OnEnable()
    {   
        m_HorizontalInputValue = 0f;    
        m_VerticalInputValue = 0f;
    }
    private void OnDisable()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        m_HorizontalInputValue = Input.GetAxis("Horizontal");
        m_VerticalInputValue = Input.GetAxis("Vertical");
        SetHealthUI();
        if (Input.GetButtonDown("Fire1"))
            isFiring = true;
        if (Input.GetButtonUp("Fire1"))
            isFiring = false;
            if (isFiring == true) //Input.GetButtonDown("JUmp")
        {
           
            ammoTextPanel.SetActive(true);
            FireWeapon();
            //Debug.Log("Fired weapon");
            TextBoxCheck();
        }
        else if (isFiring == false)
        {
            //ammoTextPanel.SetActive(false);          
        }
        if (playerAmmo <= 0)
        {
            playerAmmoText.text = "0";
        //    for (int i = 0; i > playerAmmo; playerAmmo++) ;

               
        }

    }
    private void TextBoxCheck()
    {
        if (ammoTextPanel.activeSelf)
        {
            checkPointTextTime = 1f;
            checkPointTextTime -= Time.deltaTime;
            if (checkPointTextTime <= 0)
            {
                playerAmmoText.text = "";
                ammoTextPanel.SetActive(false);
            }
        }
    }

void FireWeapon()
{
  // playerAmmo -= Time.deltaTime;

    playerAmmo--;
    playerAmmoText.text = playerAmmo.ToString();
    
}
    private void SetHealthUI()
    {
        playerHealthText.text = playerHealth.ToString();
        // Adjust the value and colour of the slider.
        //m_Slider.value = m_CurrentHealth;
        //m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }
    void TakeDamage()
    {
        playerHealth--;
        playerHealthText.text = playerHealth.ToString();
    }


}


