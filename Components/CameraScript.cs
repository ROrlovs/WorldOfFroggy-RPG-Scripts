using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Player player;
    public Enemy target;
    [SerializeField] private float cameraZOffset;


    void OnEnable()
    {
        player.onNewTarget += TargetEnemyCamera;
        player.onClearTarget += FollowPlayer;
    }

    void OnDisable() {
        player.onNewTarget -= TargetEnemyCamera;
        player.onClearTarget -= FollowPlayer;
    }

    void Start()
    {
        StartCoroutine(FollowPlayerCoroutine());
    }

    private void TargetEnemyCamera(Enemy enemy)
    {
        target = enemy;
        StartCoroutine(TargetEnemyCoroutine());
    }

    private void FollowPlayer()
    {
        target = null;
        StartCoroutine(FollowPlayerCoroutine());
    }

    private IEnumerator FollowPlayerCoroutine()
    {
        while (target==null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cameraZOffset);
            yield return new WaitForEndOfFrame();            
        }

        
        
    }

    private IEnumerator TargetEnemyCoroutine()
    {
        while (target!=null)
        {
            transform.position =  Vector2.Lerp(player.transform.position, target.transform.position, 0.5f);
            transform.position = new Vector3(transform.position.x,transform.position.y,cameraZOffset); //Force z value
            yield return new WaitForEndOfFrame();
        }

        
        
    }
}
