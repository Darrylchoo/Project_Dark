using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlatformerHealthSystem : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Vector2 interactableSize;
    [SerializeField] private Vector3 interactableOffset;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI deathText;

    public float health;
    public float maxHealth;
    public float currentPercentage;
    public float drainRate;
    public float reloadDelay;
    public bool isAlive = true;
    private Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        deathText.transform.gameObject.SetActive(false);
        health = maxHealth;
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D interaction = Physics2D.OverlapBox(transform.position + interactableOffset, interactableSize, 0, interactableLayer);

        if (interaction) health -= drainRate * Time.deltaTime;

        if (health <= 0)
        {
            health = 0;
            isAlive = false;
            StartCoroutine(ReloadScene());
        }

        currentPercentage = (health / maxHealth) * 100;
        healthText.text = "Health - " + (int)currentPercentage + "%";
    }

    private IEnumerator ReloadScene()
    {
        deathText.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(reloadDelay);
        SceneManager.LoadScene(scene.name);
        deathText.transform.gameObject.SetActive(false);
    }
}
