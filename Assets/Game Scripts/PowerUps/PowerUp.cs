using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static int total = 0;
    public float minDistance = 2f;
    void OnEnable()
    {
        RandomPos();
        StartCoroutine(Expired());
    }
    public void RandomPos() {
        Vector3 newPos;
        bool posValid = false;
        int attemps = 0;
        do {
            newPos = new Vector3(
            Random.Range(0, 15), 
            transform.position.y,
            Random.Range(0, 15)
            );
            posValid = checkValid(newPos);
            attemps ++;
            if (attemps > 50) {
                break;
            }

        } while (!posValid);
        transform.position = newPos;
    }

    public bool checkValid(Vector3 newPos) {
        // Check distance from player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(newPos, player.transform.position) < minDistance)
            return false;

        // Check distance from other power-ups
        foreach (GameObject power in GameObject.FindGameObjectsWithTag("PowerUp"))
        {
            if (power.activeSelf && Vector3.Distance(newPos, power.transform.position) < minDistance)
                return false;
        }

        return true;
    }
    IEnumerator Expired() {
        yield return new WaitForSeconds(Random.Range(3f, 10f));
        this.gameObject.SetActive(false);
        total --;
    }
}