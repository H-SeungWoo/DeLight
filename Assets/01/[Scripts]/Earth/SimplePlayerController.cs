using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClearSky
{
    public class SimplePlayerController : MonoBehaviour
    {
        public float movePower = 7f;
        public float jumpPower = 13f; //Set Gravity Scale in Rigidbody2D Component to 5
        public static bool islightstop = false;
        private Rigidbody2D rb;
        private Animator anim;
        Vector3 movement;
        private float direction;
        bool isJumping = false;
        public bool alive = true;
        public bool islight = false;
        public bool isspace = false;
        public bool isGround = false;
        public GameObject StraightOfLight ;
        public GameObject Gameover;
        public GameObject PauseCanvas;
        public GameObject ClearCan;
        public GameObject Onbulb;
        GameObject[] player;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            Gameover = GameObject.Find("Canvas").transform.Find("Gameover").gameObject;
            PauseCanvas = GameObject.Find("Canvas").transform.Find("Pause").gameObject;
            Onbulb = GameObject.Find("Canvas").transform.Find("Onbulb").gameObject;
            ClearCan = GameObject.Find("Canvas").transform.Find("Clear").gameObject;
        }

        private void Update()
        {
            Restart();
            if (alive&& !islight)
            {
                Hurt();
                Die();
                Attack();
                Jump();
                Run();

            }
            else if(islight)
            {
                Rotation(StraightOfLight);
                Shoot();
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
            if (other.tag == "Slight")
            {
                anim.SetBool("isRun", false);
                islight = true;
                this.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, this.transform.position.z);
                rb.gravityScale = 0;
                StraightOfLight = other.gameObject;
                SoundManager.instance.SFXPlay("Ride", SoundManager.instance.ride_sound,0.7f);
            }
            else if (other.tag == "bulb")
            {
                Onbulb.SetActive(true); 
                other.gameObject.SetActive(false);
                SoundManager.instance.SFXPlay("touchblub", SoundManager.instance.touchbulb_sound,0.7f);
            }
            else if (other.tag == "dark")
            {
               // player = GameObject.FindGameObjectsWithTag("Player");
               // if (player.Length <= 1)
                    Gameover.SetActive(true);
                this.gameObject.SetActive(false);
                SoundManager.instance.SFXPlay("Die", SoundManager.instance.die_sound, 1f);
            }
            else if(other.tag == "clear")
            {
                player = GameObject.FindGameObjectsWithTag("Player");
                if (player.Length <= 1)
                {
                    if (SceneManager.GetActiveScene().name == "Earth")
                    {
                        mapManager.currentMap = 1;
                    }
                    else if (SceneManager.GetActiveScene().name == "Sky")
                    {
                        mapManager.currentMap = 3;
                    }
                    else if (SceneManager.GetActiveScene().name == "Dark")
                    {
                        mapManager.currentMap = 5;
                    }
                    ClearCan.SetActive(true);
                    this.gameObject.SetActive(false);
                    SoundManager.instance.SFXPlay("clear", SoundManager.instance.clear_sound, 1f);
                }
            }
            if(other.tag == "Ground")
            {
                isGround = true;
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.tag == "Slight" && !isspace)
            {
                //other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                //other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                anim.SetBool("isRun", false);
                this.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, this.transform.position.z);
            }
            else if (other.tag == "Ground")
            {
                isGround = true;
                
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Slight")
            {
                collision.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (collision.tag == "Ground")
            {
                //isGround = false;
            }
            if(collision.tag == "wall")
            {
                //player = GameObject.FindGameObjectsWithTag("Player");
               // if(player.Length <= 1)
                    Gameover.SetActive(true);
                this.gameObject.SetActive(false);
                SoundManager.instance.SFXPlay("Die", SoundManager.instance.die_sound, 1f);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag != "Slight" )
            {
                isspace = false;
                islight = false;
                anim.SetBool("isRun", false);
                rb.gravityScale = 5;
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);


            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = 0.09575041f;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction,this.transform.localScale.y, this.transform.localScale.z);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

                if (SoundManager.instance.run_sound_timer > 30)
                {
                    SoundManager.instance.SFXPlay("run", SoundManager.instance.run_sound,0.3f);
                    SoundManager.instance.run_sound_timer = 0;

                }

            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = -0.09575041f;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, this.transform.localScale.y, this.transform.localScale.z);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

                if (SoundManager.instance.run_sound_timer > 30)
                {
                    SoundManager.instance.SFXPlay("run", SoundManager.instance.run_sound,0.3f);
                    SoundManager.instance.run_sound_timer = 0;

                }
            }

            SoundManager.instance.run_sound_timer++;

            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
        void Jump()
        {
            //Debug.Log(Input.GetAxisRaw("Vertical"));
            //Debug.Log(!anim.GetBool("isJump"));

            //Debug.Log((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0) && !anim.GetBool("isJump"));
            if ((Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical") > 0)
            && !anim.GetBool("isJump")&& isGround)
            {
                isJumping = true;
                anim.SetBool("isJump", true);
                SoundManager.instance.SFXPlay("Jump", SoundManager.instance.jump_sound,0.5f);
                isGround = false;
            }
            else if(Input.GetAxisRaw("Vertical") <= 0 )
            {
                anim.SetBool("isJump", false);
            }
            if (!isJumping)
            {
                return;
            }
            rb.velocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
            
            isJumping = false;
        }
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("attack");
                SoundManager.instance.SFXPlay("Attack", SoundManager.instance.attack_sound,0.7f);
            }
        }
        void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }
        void Die()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                anim.SetTrigger("die");
                alive = false;

                SoundManager.instance.SFXPlay("Die", SoundManager.instance.die_sound, 1f);
            }
        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
        }
        void Rotation(GameObject StraightOfLight)
        {
            if (this.transform.rotation.z <= 0.7)
            {
                Debug.Log(this.transform.rotation.z);
                if (Input.GetKey(KeyCode.A))
                {
                    transform.eulerAngles += new Vector3(0, 0, 0.5f);
                    StraightOfLight.transform.eulerAngles += new Vector3(0, 0, 0.5f);
                    if (SoundManager.instance.rotate_sound_timer > 40)
                    {
                        SoundManager.instance.SFXPlay("rotate", SoundManager.instance.rotate_sound, 0.3f);
                        SoundManager.instance.rotate_sound_timer = 0;

                    }
                }
            }
            if (this.transform.rotation.z >= -0.7 )
            {
                if (Input.GetKey(KeyCode.D))
                {
                    transform.eulerAngles -= new Vector3(0, 0, 0.5f);
                    StraightOfLight.transform.eulerAngles -= new Vector3(0, 0, 0.5f);
                    if (SoundManager.instance.rotate_sound_timer > 40)
                    {
                        SoundManager.instance.SFXPlay("rotate", SoundManager.instance.rotate_sound,0.3f);
                        SoundManager.instance.rotate_sound_timer = 0;

                    }
                }
            }
            SoundManager.instance.rotate_sound_timer++;

        }

        void Shoot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isspace = true;
               // 미사일을 전방으로 발사
                rb.AddForce(transform.up * 40, ForceMode2D.Impulse);
                SoundManager.instance.SFXPlay("Shoot", SoundManager.instance.shoot_sound,0.6f);
            }
        }

        public void Pause()
        {
            
                PauseCanvas.SetActive(true);
                if (Time.timeScale == 1.0F)
                    Time.timeScale = 0.02F;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            
        }


    }
}