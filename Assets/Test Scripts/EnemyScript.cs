using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour {

    // Use this for initialization
   
    [SerializeField]
    Text enemyHealthText;
    [SerializeField]
    GameObject HealthPanel;

    int enemyHealth = 100;
    float checkPointTextTime = 2.5f;
    public float m_StartingHealth = 100f;
    public float m_CurrentHealth;
    private bool m_Dead;
    void Start()
    {
        m_CurrentHealth = m_StartingHealth;
    }
    void Update()
    {
        TextBoxCheck();
    }
    private void TextBoxCheck()
    {
        if (HealthPanel.activeSelf)
        {
            //checkPointTextTime = 1f;
            //checkPointTextTime -= Time.deltaTime;
            m_CurrentHealth = enemyHealth;
            enemyHealthText.text = enemyHealth.ToString();
            //if (checkPointTextTime <= 0) change to if dead or target Inactive
            //{
            //    playerAmmoText.text = "";
            //    HealthPanel.SetActive(false);
            //}
        }
    }

    // Update is called once per frame
    
}
