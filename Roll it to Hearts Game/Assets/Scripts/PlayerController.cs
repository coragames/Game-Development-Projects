using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
 
       private Rigidbody rb; 
       public int count;
       public int totalCounts;
       private float movementX;
       private float movementY; 
       public float speed = 0;
       public TextMeshProUGUI countText;
       public TextMeshProUGUI totalCountText;
       public GameObject winTextObject;
       public GameObject sparkleHeart;
       public GameObject winSparkle;
       private AudioSource sfxPlay;
       public AudioClip pickupMusic;
       public AudioClip playerWin;   
       public GameObject nextLevel;
       public GameObject bgMusic;
       public GameObject playerLoose;
       public GameObject playerWinner;  
       public TextMeshProUGUI playerName; 

       
 // Start is called before the first frame update.
void Start()
   {
       rb = GetComponent<Rigidbody>();
       count = 0;
       totalCounts = GameManager.Instance.levelScoreGM;
   
       SetCountText();
       GameManager.Instance.LoadData();
            
       sfxPlay = GetComponent<AudioSource>();  

       playerName.text = PlayerPrefs.GetString("playerName");
   }
  
void OnMove(InputValue movementValue)
    {
       Vector2 movementVector = movementValue.Get<Vector2>();

       movementX = movementVector.x; 
       movementY = movementVector.y; 
       
    }

 private void FixedUpdate() 
    { 
       Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
       rb.AddForce(movement * speed); 
    }

private void OnCollisionEnter(Collision collision)
    {  
       if (collision.gameObject.CompareTag("Enemy"))
       {      
              Destroy(gameObject); 

              winTextObject.gameObject.SetActive(true);
              winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!"; 
              bgMusic.gameObject.SetActive(false);
              playerLoose.gameObject.SetActive(true);                  
       }
    }
     
void OnTriggerEnter(Collider other) 
    { 
       if (other.gameObject.CompareTag("PickUp")) 
       {       
              other.gameObject.SetActive(false);       
              count += 1;                 

              GameManager.Instance.levelScoreGM += 1;   
              totalCounts = GameManager.Instance.levelScoreGM;

              SetCountText();

              sfxPlay.PlayOneShot(pickupMusic, 1);
              Instantiate(sparkleHeart, transform.position, sparkleHeart.transform.rotation);
       }
    }

void SetCountText() 
    {
       countText.text = "Hearts: " + count.ToString();
       totalCountText.text = "Total Hearts: " + totalCounts.ToString();      

       if (count >= 12)
       {

              winTextObject.SetActive(true);
              sfxPlay.PlayOneShot(playerWin, 1);

              Destroy(GameObject.FindGameObjectWithTag("Enemy"));

              nextLevel.gameObject.SetActive(true);
              playerWinner.gameObject.SetActive(true);
              bgMusic.gameObject.SetActive(false);
              winSparkle.gameObject.SetActive(true);              

              GameManager.Instance.WriteData();
       }
    }
}