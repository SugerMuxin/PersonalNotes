using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    public GameObject root;
    IPlayer player;
    void Start()
    {
         //if(root.GetComponent<Player>().enabled)
         //{
         //    player = root.GetComponent<Player>() as IPlayer;
         //}
         //else if (root.GetComponent<RemotePlayer>().enabled)
         //{
         //    player = root.GetComponent<RemotePlayer>() as IPlayer;
         //}
      
    }

    public void init()
    {
        //if (root.GetComponent<Player>().enabled)
        //{
        //    player = root.GetComponent<Player>() as IPlayer;
        //}
        //else if (root.GetComponent<RemotePlayer>().enabled)
        //{
        //    player = root.GetComponent<RemotePlayer>() as IPlayer;
        //}
    }

   

    void OnTriggerEnter(Collider other)
    {
        //if (player != null && player.gameObject != other.gameObject)
        //{
        //    player.Attack(other.gameObject.transform); 
        //}
       
    }

    void OnTriggerExit(Collider other)
    {
        //if (player != null)  
        //{
        //   player.DisAttack();
        //}
    }
}
