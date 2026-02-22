using UnityEngine;

public class GazeInteraction : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;

    public Color gazeColor = Color.yellow;
    public float dwellTime = 2f;

    public AudioSource selectAudio; // ✅ move inside the class

    private float timer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;

        // Optional auto-get if you forgot to assign in Inspector:
        if (selectAudio == null)
            selectAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            objectRenderer.material.color = gazeColor;
            timer += Time.deltaTime;

            if (timer >= dwellTime)
            {
                if (selectAudio != null) selectAudio.Play(); // ✅ avoid null crash
                transform.localScale *= 1.3f;
                timer = 0;
            }
        }
        else
        {
            objectRenderer.material.color = originalColor;
            timer = 0;
        }
    }
}