using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Player player;
    public Enemy target;
    [SerializeField] private float cameraZOffset;
    private Vector3 velocity = Vector3.zero;


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
        if(target.isTargetable) StartCoroutine(TargetEnemyCoroutine());
        else target=null;
        
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
            transform.position = Vector3.SmoothDamp(transform.position,new Vector3(player.transform.position.x, player.transform.position.y, cameraZOffset),ref velocity,0.25f) ;
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
