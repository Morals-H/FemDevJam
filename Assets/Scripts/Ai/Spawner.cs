using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject Creature;

    //how many are and can be spawned
    public int spawned;
    public int spawnMaxConcurrent;

    //how far a spawn can be away
    public Vector2 minRadius;
    public Vector2 maxRadius;
    public int spawnRate;
    private int Timer;

    private void Start()
    {

        maxRadius /= 2;
        minRadius /= 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawned < spawnMaxConcurrent && Timer <= 0)
        {
            Vector2 myPoint = new Vector2(Random.Range(maxRadius.x, -maxRadius.x) + transform.position.x,
            Random.Range(maxRadius.y, -maxRadius.y) + transform.position.y);

            //making sure x isn't to close
            if (myPoint.x < 0 && myPoint.x > minRadius.x)
            {
                myPoint.x = -minRadius.x;
            }
            else if (myPoint.x > 0 && myPoint.x < minRadius.x)
            {
                myPoint.x = minRadius.x;
            }

            //making sure y isn't to close
            if (myPoint.y < 0 && myPoint.y > minRadius.y)
            {
                myPoint.y = -minRadius.y;
            }
            else if (myPoint.y > 0 && myPoint.y < minRadius.y)
            {
                myPoint.y = minRadius.y;
            }

            //checking if position is on nav mesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(myPoint, out hit, 10.0f, NavMesh.AllAreas))
            {
                myPoint = new Vector3(hit.position.x, hit.position.y, 0);
            }
            else return;

            //creating game object
            GameObject myChild = Instantiate(Creature, myPoint, Quaternion.identity);
            myChild.GetComponent<Ai_Core>().mom = this;

            spawned++;

            Timer = Mathf.RoundToInt(spawnRate * Random.Range(0.5f, 1.5f));
        }
        if (Timer > 0 && spawned != spawnMaxConcurrent) Timer--;
    }
}
