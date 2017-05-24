using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EnemyScript : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    Text playerAmmoText;
    [SerializeField]
    Text enemyHealthText;
    [SerializeField]
    GameObject HealthPanel;

    int enemyHealth = 100;
    float checkPointTextTime = 2.5f;
    public float m_StartingHealth = 100f;
    public float m_CurrentHealth;
    private bool m_Dead;
    private void TextBoxCheck()
    {
        if (HealthPanel.activeSelf)
        {
            checkPointTextTime = 1f;
            checkPointTextTime -= Time.deltaTime;
            enemyHealthText.text = enemyHealth.ToString();
            if (checkPointTextTime <= 0)
            {
                playerAmmoText.text = "";
                HealthPanel.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
