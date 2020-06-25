using System.Collections;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    public GameObject bloodParticles;
    public GameObject weaponParticles;
    public int damage = 34;

    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var point = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0);
            var ray = camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var hitObject = hit.transform.gameObject;
                var zombieController = hitObject.GetComponent<ZombieController>();
                StartCoroutine(CreateParticles(hit.point, weaponParticles));
                if (zombieController != null)
                {
                    HitZombie(zombieController, hit.point);
                }
            }
        }
    }

    private void OnGUI()
    {
        var size = 20;
        float x = camera.pixelWidth / 2 - size / 4;
        float y = camera.pixelHeight / 2 - size / 2;

        GUI.Label(new Rect(x, y, size, size), "+");
    }

    private void HitZombie(ZombieController zombieController, Vector3 position)
    {
        StartCoroutine(CreateParticles(position, bloodParticles));
        zombieController.ReactToHit(damage);
    }

    private IEnumerator CreateParticles(Vector3 position, GameObject particles)
    {
        var particleObject = Instantiate(particles, position, Quaternion.identity);

        yield return new WaitForSeconds(1);

        Destroy(particleObject);
    }
}
